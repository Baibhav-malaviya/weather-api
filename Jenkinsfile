pipeline {

    agent any

    environment {
        DOCKERHUB_USER      = "baibhavkumar07"
        API_IMAGE           = "baibhavkumar07/weather-api"
        UI_IMAGE            = "baibhavkumar07/weather-ui"
        IMAGE_TAG           = "${BUILD_NUMBER}"
        SONAR_PROJECT_KEY   = "weather-api"
        SONAR_HOST_URL      = "http://host.docker.internal:9000"
    }

    stages {

        stage('Checkout') {
            steps {
                git branch: 'main',
                    credentialsId: 'github-pat',
                    url: 'https://github.com/Baibhav-malaviya/weather-api.git'
            }
        }

        stage('SonarQube Analysis (.NET API)') {
            agent {
                docker {
                    image 'mcr.microsoft.com/dotnet/sdk:10.0'
                    args '-u root'
                    reuseNode true
                }
            }
            steps {
                withCredentials([string(credentialsId: 'sonar-token', variable: 'SONAR_TOKEN')]) {
                    sh '''
                        set -e

                        apt-get update
                        apt-get install -y --no-install-recommends openjdk-17-jre-headless
                        rm -rf /var/lib/apt/lists/*

                        dotnet tool install --global dotnet-sonarscanner || true
                        export PATH="$PATH:/root/.dotnet/tools"
                        export JAVA_HOME=/usr/lib/jvm/java-17-openjdk-amd64

                        dotnet sonarscanner begin \
                          /k:"$SONAR_PROJECT_KEY" \
                          /d:sonar.host.url="$SONAR_HOST_URL" \
                          /d:sonar.login="$SONAR_TOKEN"

                        dotnet build WeatherAppSolution/WeatherApp/WeatherApp.csproj -c Release

                        dotnet sonarscanner end \
                          /d:sonar.login="$SONAR_TOKEN"
                    '''
                }
            }
        }

        stage('Build Docker Images') {
            steps {
                sh '''
                    docker build -t $API_IMAGE:$IMAGE_TAG \
                        ./WeatherAppSolution/WeatherApp

                    docker build -t $UI_IMAGE:$IMAGE_TAG \
                        ./weather-ui
                '''
            }
        }

        stage('Docker Login') {
            steps {
                withCredentials([usernamePassword(
                    credentialsId: 'dockerhub-creds',
                    usernameVariable: 'DOCKER_USER',
                    passwordVariable: 'DOCKER_PASS'
                )]) {
                    sh '''
                        echo "$DOCKER_PASS" | docker login \
                            -u "$DOCKER_USER" --password-stdin
                    '''
                }
            }
        }

        stage('Push Docker Images') {
            steps {
                sh '''
                    docker push $API_IMAGE:$IMAGE_TAG
                    docker push $UI_IMAGE:$IMAGE_TAG

                    docker tag $API_IMAGE:$IMAGE_TAG $API_IMAGE:latest
                    docker tag $UI_IMAGE:$IMAGE_TAG $UI_IMAGE:latest

                    docker push $API_IMAGE:latest
                    docker push $UI_IMAGE:latest
                '''
            }
        }
    }

    post {
        always {
            sh 'docker logout || true'
            cleanWs()
        }
    }
}