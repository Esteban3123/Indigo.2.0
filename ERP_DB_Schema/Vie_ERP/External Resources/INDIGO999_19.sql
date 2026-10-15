CREATE EXTERNAL DATA SOURCE [INDIGO999_19]
    WITH (
    TYPE = RDBMS,
    LOCATION = N'ssindigodev.database.windows.net',
    DATABASE_NAME = N'INDIGO999_19',
    CREDENTIAL = [LoginExternalTablesSEC]
    );

