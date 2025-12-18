# 🚀 Railway Deployment Guide

## Step 1: Create Railway Account
1. Go to https://railway.app
2. Sign up with GitHub account (recommended)

## Step 2: Deploy Your Backend
1. Click "New Project"
2. Select "Deploy from GitHub repo"
3. Connect your GitHub account
4. Upload your Backend folder to a GitHub repository
5. Select the repository in Railway

## Step 3: Configure Environment Variables
In Railway dashboard, add these environment variables:
- `DATABASE_URL` = `Data Source=db29589.public.databaseasp.net;Initial Catalog=db29589;User ID=db29589_prathmesh;Password=7b_G=Yk8J6!d;Pooling=False;Multiple Active Result Sets=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Authentication=SqlPassword;Connect Retry Count=1;Connect Retry Interval=10;Command Timeout=30`
- `ASPNETCORE_ENVIRONMENT` = `Production`

## Step 4: Your API Will Be Live At:
Railway will provide a URL like: `https://your-app-name.up.railway.app`

## Step 5: Test Your Endpoints:
- `https://your-app-name.up.railway.app/api/restaurants`
- `https://your-app-name.up.railway.app/api/menuitems`
- `https://your-app-name.up.railway.app/api/cart`
- `https://your-app-name.up.railway.app/api/checkout`
- `https://your-app-name.up.railway.app/api/orders`
- `https://your-app-name.up.railway.app/api/addresses`

## Alternative: Direct Deploy
1. Install Railway CLI: `npm install -g @railway/cli`
2. Login: `railway login`
3. In your project folder: `railway deploy`

Your .NET 6 backend will work perfectly on Railway!