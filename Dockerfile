# Sử dụng image cơ bản từ .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set thư mục làm việc
WORKDIR /app

# Copy file .csproj và restore các dependency
COPY *.csproj ./
RUN dotnet restore

# Copy toàn bộ mã nguồn vào container
COPY . ./

# Build ứng dụng
RUN dotnet publish -c Release -o /out

# Sử dụng image cơ bản cho ứng dụng đã được build (runtime image)
FROM mcr.microsoft.com/dotnet/aspnet:6.0

# Set thư mục làm việc
WORKDIR /app

# Copy ứng dụng đã build từ image build trước đó
COPY --from=build /out .

# Mở port 80 để API có thể truy cập
EXPOSE 80

# Chạy ứng dụng khi container được start
ENTRYPOINT ["dotnet", "WebApi.dll"]