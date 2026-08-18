# Hosting ASP.NET Core Applications — Local Docker & Free Hosting

This document shows steps to:
- Build and run `WebApp13BySupriya` locally using Docker (for part a).
- Deploy `WebApp5BySupriya` to a free hosting platform (Render) (for part b).

Placeholders for screenshots are included; after running the commands capture screenshots and save them under `screenshots/`.

Prerequisites
- Docker installed and running locally.
- GitHub account (for Render), or another Git provider.

1) Local Docker hosting (WebApp13BySupriya)

- Open a terminal in the project folder:

```bash
cd "WebApp13BySupriya"
```

- Build the Docker image:

```bash
docker build -t webapp13:local .
```

- Run the container mapping port 8080 on host to container port 80:

```bash
docker run --rm -p 8080:80 --name webapp13 webapp13:local
```

- Verify in browser: http://localhost:8080
- Screenshot suggestions:
  - After `docker build` completes (save as `screenshots/local-build-complete.png`).
  - The running container list showing `webapp13` (run `docker ps`) (save as `screenshots/docker-ps.png`).
  - The app running in the browser showing home page (save as `screenshots/local-browser.png`).

2) Deploying `WebApp5BySupriya` to Render (free tier)

Overview: push your project to a GitHub repo, connect Render, and create a new Web Service using the existing `Dockerfile`.

Steps:

- Create a Git repository and commit the project (from workspace root):

```bash
cd "WebApp5BySupriya"
git init
git add .
git commit -m "Add WebApp5 for Render deployment"
# create a GitHub repo and push — replace URL below with your repo
git remote add origin <your-github-repo-url>
git branch -M main
git push -u origin main
```

- On Render.com:
  1. Sign in and click "New" → "Web Service".
  2. Connect GitHub and pick the repository you pushed.
  3. Set the environment to "Docker" (Render will use the Dockerfile in the repo).
  4. Set the build and start commands if Render asks (Dockerfile-based services usually need none).
  5. Set the service to use port 80 (the container listens on 80).
  6. Create the service and wait for build + deploy to finish.

- After deploy completes, open the provided URL to verify the app is live.

- Screenshot suggestions:
  - The GitHub repo after push (`screenshots/webapp5-github.png`).
  - The Render service creation screen (`screenshots/render-create.png`).
  - The Render deploy logs showing successful build (`screenshots/render-logs.png`).
  - The live site in browser (`screenshots/render-live.png`).

Notes and troubleshooting
- If the app uses a database, configure environment variables and add external DB (Render offers managed Postgres on paid plans). For simple demos using SQLite the database file must be created at runtime or persisted using a writable volume.
- If Render fails to detect the port mapping, ensure the Dockerfile exposes port 80 and the app listens on port 80 (the included Dockerfiles set `ASPNETCORE_URLS` to `http://+:80`).

Files added by these instructions:
- `WebApp13BySupriya/Dockerfile` — local Docker multi-stage build
- `WebApp5BySupriya/Dockerfile` — Dockerfile for hosting on Render
- `screenshots/` — suggested place to store screenshots (create locally after capturing images)

Optional: Docker Compose

Create `docker-compose.yml` at workspace root to run the app with `docker compose up` mapping port 8080:

```yaml
version: '3.4'
services:
  webapp13:
    build: ./WebApp13BySupriya
    image: webapp13:local
    ports:
      - "8080:80"

```

Run:

```bash
docker compose up --build
```
