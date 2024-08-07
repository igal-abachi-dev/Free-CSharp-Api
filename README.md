# Free CSharp Api hosting 
.net 8 web api , c#

test free render.com
api hosting using docker

uses visual studio web api template(weather forcast)

live url:

https://apitest-v059.onrender.com/WeatherForecast/


 connect free
https://uptimerobot.com/
so it wont spin down(50sec delay on first call)


# Deploying a C# Web API on Render.com Using Docker

## Step 1: Create a Render Account

1. Go to [Render.com](https://render.com/) and sign up for a free account.
2. After signing up, log in to your Render dashboard.

## Step 2: Create a New Web Service

1. In the Render dashboard, click on the "New" button and select "Web Service".
2. Connect your GitHub repository containing your C# web API project to Render.

## Step 3: Configure the Service

1. **Name**: Give your service a name.
2. **Environment**: Select Docker.
3. **Build Command**: Leave this blank as the Dockerfile handles the build process.
4. **Start Command**: Leave this blank as the Dockerfile specifies the ENTRYPOINT.
5. **Dockerfile Path**: If your Dockerfile is in the root of the repository, leave this as `Dockerfile`.
6. **Docker Context Directory**: If your Dockerfile is in the root, set this to `/`.

## Step 4: Deploy

1. Click on "Create Web Service".
2. Render will start the build process, pulling the necessary images, building your project, and deploying the service.

Your web service should now be up and running on Render.com!
