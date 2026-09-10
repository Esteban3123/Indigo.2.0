
DECLARE @EnvironmentNameInsertion VARCHAR(50) = 'Debug'
DECLARE @ContainerId INT = 99
DECLARE @EnvironmentName VARCHAR(50) = 'Debug'
DECLARE @AppName VARCHAR(100) = 'Snorlax'


INSERT INTO [Application].AppSettings ([Key], [Value], [Type], CreatedFor)
 VALUES ('MONGOCOLLECTIONSNL', 'HistoryFor',@EnvironmentNameInsertion, @AppName)

INSERT INTO [Application].AppSettings ([Key], [Value], [Type], CreatedFor)
 VALUES ('MONGODATABASENAMEV2', 'ControlReport',@EnvironmentNameInsertion, @AppName)

INSERT INTO [Application].AppSettings ([Key], [Value], [Type], CreatedFor)
 VALUES ('MONGODATABASENAMEV2LEGACY', 'HistoryPrint', @EnvironmentNameInsertion, @AppName)

INSERT INTO [Application].AppSettings ([Key], [Value], [Type], CreatedFor)
 VALUES ('BLOBCONTAINERV2', 'pdfs', @EnvironmentNameInsertion, @AppName)

INSERT INTO [Application].AppSettings ([Key], [Value], [Type], CreatedFor)
 VALUES ('RATELIMITERV2', 'false', @EnvironmentNameInsertion, @AppName)

INSERT INTO [Application].AppSettings ([Key], [Value], [Type], CreatedFor)
 VALUES ('APPLICATION_ENTRYPOINT', 'http://localhost:8081', @EnvironmentNameInsertion, @AppName)

INSERT INTO [Application].AppSettings ([Key], [Value], [Type], CreatedFor)
  VALUES ('ISDBENCRYPT', 'false', @EnvironmentNameInsertion, @AppName)

INSERT INTO [Application].AppSettings ([Key], [Value], [Type], CreatedFor)
  VALUES ('DBENCRYPTPASS', 'AQUI LA CLAVE PERTINENTE', @EnvironmentNameInsertion, @AppName)

INSERT INTO [Application].AppSettings ([Key], [Value], [Type], CreatedFor)
 VALUES ('MONGODB', 'AQUI LA CADENA DE CONEXION PERTINENTE',@EnvironmentNameInsertion,@AppName)

INSERT INTO [Application].AppSettings ([Key], [Value], [Type], CreatedFor)
 VALUES ('BLOBSTORAGE', 'AQUI LA CADENA DE CONEXION PERTINENTE', @EnvironmentNameInsertion, @AppName)

 INSERT INTO [Application].AppSettings ([Key], [Value], [Type], CreatedFor)
  VALUES ('HEIMDALL_URL', 'https://col-qa-heimdall-api.azurewebsites.net/oauth2/NativeToken2', @EnvironmentNameInsertion, @AppName)

INSERT INTO [Application].AppSettings ([Key], [Value], [Type], CreatedFor)
  VALUES ('HEIMDALL_CLIENT_ID', 'AQUI LA CLAVE PERTINENTE', @EnvironmentNameInsertion, @AppName)

INSERT INTO [Application].AppSettings ([Key], [Value], [Type], CreatedFor)
  VALUES ('GRANT_TYPE', 'client_credentials', @EnvironmentNameInsertion,@AppName)

INSERT INTO [Application].AppSettings ([Key], [Value], [Type], CreatedFor)
  VALUES ('HEIMDALL_ENCRYPTEDKEY', 'AQUI LA CLAVE PERTINENTE', @EnvironmentNameInsertion,@AppName)

INSERT INTO [Application].EnvironmentSpaces (Environment, AppName, ContainerId)
  VALUES (@EnvironmentName, @AppName ,@ContainerId)


DECLARE @EnvironmentId INT

SELECT @EnvironmentId = Id
FROM [Application].EnvironmentSpaces 
WHERE ContainerId = @ContainerId AND Environment = @EnvironmentName AND AppName = @AppName

IF @EnvironmentId IS NULL
BEGIN
    INSERT INTO [Application].EnvironmentSpaces (Environment, AppName, ContainerId)
    VALUES (@EnvironmentName, @AppName, @ContainerId)

    SET @EnvironmentId = SCOPE_IDENTITY()
END

DECLARE @Keys TABLE ([Key] VARCHAR(100))

INSERT INTO @Keys ([Key])
VALUES
 ('ISDBENCRYPT'),
 ('DBENCRYPTPASS'),
 ('BLOBCONTAINERV2'),
 ('MONGODATABASENAMEV2'),
 ('MONGOCOLLECTIONSNL'),
 ('MONGODATABASENAMEV2LEGACY'),
 ('BLOBSTORAGE'),
 ('MONGODB'),
 ('RATELIMITERV2'),
 ('HEIMDALL_URL'),
 ('HEIMDALL_CLIENT_ID'),
 ('GRANT_TYPE'),
 ('HEIMDALL_ENCRYPTEDKEY')

DECLARE @CurrentKey VARCHAR(100)
DECLARE @SettingId INT

DECLARE KeyCursor CURSOR FOR
    SELECT [Key] FROM @Keys

OPEN KeyCursor
FETCH NEXT FROM KeyCursor INTO @CurrentKey

WHILE @@FETCH_STATUS = 0
BEGIN


    SELECT @SettingId = Id FROM [Application].AppSettings WHERE [Key] = @CurrentKey

    IF @SettingId IS NOT NULL
    BEGIN
        IF NOT EXISTS (
            SELECT 1 FROM [Application].SettingAssignments 
            WHERE SettingId = @SettingId AND EnvironmentId = @EnvironmentId
        )
        BEGIN
            INSERT INTO [Application].SettingAssignments (EnvironmentId, SettingId)
            VALUES (@EnvironmentId, @SettingId)
        END
    END
    ELSE
    BEGIN
        PRINT 'no hay clave para: ' + @CurrentKey
    END

    FETCH NEXT FROM KeyCursor INTO @CurrentKey
END

CLOSE KeyCursor
DEALLOCATE KeyCursor

 SELECT *
                FROM [Application].AppSettings s
               INNER JOIN [Application].SettingAssignments sa ON sa.SettingId = s.Id
                INNER JOIN [Application].EnvironmentSpaces e ON e.Id = sa.EnvironmentId
                WHERE e.ContainerId = @ContainerId AND e.Environment = @EnvironmentName


--CREATE SCHEMA [Application];
--CREATE TABLE [Application].AppSettings (
--    Id INT IDENTITY PRIMARY KEY,
--    [Key] VARCHAR(100) NOT NULL,
--    [Value] VARCHAR(MAX) NOT NULL,
--    [Type] VARCHAR(50) NOT NULL,
--    CreatedFor VARCHAR(50) NOT NULL
--);
--CREATE TABLE [Application].EnvironmentSpaces (
--    Id INT IDENTITY PRIMARY KEY,
--    Environment VARCHAR(50), 
--    AppName VARCHAR(100),
--    ContainerId INT NOT NULL
--);
--CREATE TABLE [Application].SettingAssignments (
--    Id INT IDENTITY PRIMARY KEY,
--    EnvironmentId INT NOT NULL,
--    SettingId INT NOT NULL
--);


