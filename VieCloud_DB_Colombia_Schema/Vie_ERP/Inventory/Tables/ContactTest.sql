CREATE TABLE [Inventory].[ContactTest] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [Code]             VARCHAR (25)  NOT NULL,
    [Name]             VARCHAR (150) NOT NULL,
    [Status]           TINYINT       NOT NULL,
    [CreationDate]     DATETIME      NOT NULL,
    [CreationUser]     CHAR (20)     NOT NULL,
    [ModificationDate] DATETIME      NULL,
    [ModificationUser] CHAR (20)     NULL,
    [TimeStamp]        ROWVERSION    NOT NULL,
    CONSTRAINT [PK_AdmissionType] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tabla del esquema `Inventory` que almacena un catálogo de contactos de prueba, identificados por un código y nombre. Registra metadatos de auditoría como fechas y usuarios de creación y modificación, además de un campo `Status` para controlar la vigencia del registro. La columna `ROWVERSION` permite el control de concurrencia optimista.', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'TABLE', @level1name=N'ContactTest';
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DescriptionSource', @value=N'ai_claude-sonnet-4-6_2026-05-05', @level0type=N'SCHEMA', @level0name=N'Inventory', @level1type=N'TABLE', @level1name=N'ContactTest';
GO
