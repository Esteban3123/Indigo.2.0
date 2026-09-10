CREATE EXTERNAL DATA SOURCE [INDIGOSEC]
    WITH (
    TYPE = RDBMS,
    LOCATION = N'ssindigodev.database.windows.net',
    DATABASE_NAME = N'INDIGOSEC',
    CREDENTIAL = [LoginExternalTablesSEC]
    );

