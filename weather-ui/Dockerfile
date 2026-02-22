# ---------- Build Stage ----------
FROM node:20-alpine AS build

WORKDIR /app

# Copy dependency files first (better caching)
COPY package*.json ./

RUN npm install

# Copy rest of project
COPY . .

# Build Angular production bundle
RUN npm run build 


# ---------- Runtime Stage ----------
FROM nginx:alpine

# Remove default nginx files
RUN rm -rf /usr/share/nginx/html/*

# Replace <your-app-name> with actual folder inside dist
COPY --from=build /app/dist/weather-ui/browser /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]