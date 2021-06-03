FROM mcr.microsoft.com/dotnet/sdk:5.0 as build
WORKDIR /app

COPY *.sln .
COPY Directory.Build.props .

# Base
COPY BLL.Base/*.csproj ./BLL.Base/
COPY Contracts.BLL.Base/*.csproj ./Contracts.BLL.Base/
COPY Contracts.DAL.Base/*.csproj ./Contracts.DAL.Base/
COPY Contracts.Domain.Base/*.csproj ./Contracts.Domain.Base/
COPY DAL.Base/*.csproj ./DAL.Base/
COPY DAL.Base.EF/*.csproj ./DAL.Base.EF/
COPY Domain.Base/*.csproj ./Domain.Base/
COPY Extensions.Base/*.csproj ./Extensions.Base/

# App
COPY WebApp/*.csproj ./WebApp/
COPY BLL.App/*.csproj ./BLL.App/
COPY BLL.App.DTO/*.csproj ./BLL.App.DTO/
COPY Contracts.BLL.App/*.csproj ./Contracts.BLL.App/
COPY Contracts.DAL.App/*.csproj ./Contracts.DAL.App/
COPY DAL.App.DTO/*.csproj ./DAL.App.DTO/
COPY DAL.App.EF/*.csproj ./DAL.App.EF/
COPY Domain.App/*.csproj ./Domain.App/
COPY PublicAPI.DTO.v1/*.csproj ./PublicAPI.DTO.v1/

RUN dotnet restore

# Base
COPY BLL.Base/. ./BLL.Base/
COPY Contracts.BLL.Base/. ./Contracts.BLL.Base/
COPY Contracts.DAL.Base/. ./Contracts.DAL.Base/
COPY Contracts.Domain.Base/. ./Contracts.Domain.Base/
COPY DAL.Base/. ./DAL.Base/
COPY DAL.Base.EF/. ./DAL.Base.EF/
COPY Domain.Base/. ./Domain.Base/
COPY Extensions.Base/. ./Extensions.Base/

# App
COPY WebApp/. ./WebApp/
COPY BLL.App/. ./BLL.App/
COPY BLL.App.DTO/. ./BLL.App.DTO/
COPY Contracts.BLL.App/. ./Contracts.BLL.App/
COPY Contracts.DAL.App/. ./Contracts.DAL.App/
COPY DAL.App.DTO/. ./DAL.App.DTO/
COPY DAL.App.EF/. ./DAL.App.EF/
COPY Domain.App/. ./Domain.App/
COPY PublicAPI.DTO.v1/. ./PublicAPI.DTO.v1/

WORKDIR /app/WebApp

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 as runtime
WORKDIR /app
COPY --from=build /app/WebApp/out ./

ENTRYPOINT ["dotnet", "WebApp.dll"]