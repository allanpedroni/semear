FROM mcr.microsoft.com/mssql/server:2017-latest
COPY . /

# ENV SA_PASSWORD=Testando@123
# ENV ACCEPT_EULA=Y

RUN chmod +x /db-init.sh
CMD /bin/bash ./entrypoint.sh