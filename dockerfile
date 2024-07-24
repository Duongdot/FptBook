# Sử dụng hình ảnh cơ sở từ Microsoft
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.7.2-windowsservercore-ltsc2019 AS base
WORKDIR /inetpub/wwwroot

# Giai đoạn build
FROM mcr.microsoft.com/dotnet/framework/aspnet:4.7.2-windowsservercore-ltsc2019 AS build
WORKDIR /src
COPY . .

# Khôi phục các gói NuGet
RUN nuget restore FptBookNew1.sln

# Build dự án
RUN msbuild /p:Configuration=Release FptBookNew1.sln

# Copy build output từ stage build vào stage base
FROM base AS final
WORKDIR /inetpub/wwwroot
COPY --from=build /src/FptBookNew1/bin/Release .

# Khởi động ứng dụng ASP.NET
ENTRYPOINT ["C:\\ServiceMonitor.exe", "w3svc"]