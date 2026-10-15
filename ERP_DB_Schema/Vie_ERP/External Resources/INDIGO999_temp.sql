CREATE EXTERNAL DATA SOURCE [INDIGO999_temp]
    WITH (
    TYPE = RDBMS,
    LOCATION = N'ssindigodev.database.windows.net',
    DATABASE_NAME = N'INDIGO999_temp',
    CREDENTIAL = [LoginExternalTablesSEC]
    );

