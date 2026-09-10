#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat 

FROM mcr.microsoft.com/dotnet/framework/wcf:4.8-windowsservercore-ltsc2019
ARG source
RUN powershell -NoProfile -Command Remove-Item -Recurse C:\inetpub\wwwroot\*
RUN dism /online /enable-feature /featurename:WCF-http-Activation45 /all
ADD commands.ps1 ./commands.ps1
RUN powershell -File ./commands.ps1
WORKDIR /inetpub/wwwroot
COPY ${source:-obj/Docker/publish} .
EXPOSE 443