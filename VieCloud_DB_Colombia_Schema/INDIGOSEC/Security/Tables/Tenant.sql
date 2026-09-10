CREATE TABLE [Security].[Tenant] (
    [Id]                  SMALLINT      IDENTITY (1, 1) NOT NULL,
    [Name]                VARCHAR (200) NOT NULL,
    [Status]              TINYINT       NOT NULL,
    [WorkFlowStatus]      TINYINT       NOT NULL,
    [CompanyType]         TINYINT       CONSTRAINT [DF_Tenant_CompanyType] DEFAULT ((1)) NOT NULL,
    [CompanyNit]          VARCHAR (15)  NOT NULL,
    [RepresentationLegal] VARCHAR (100) NOT NULL,
    [CountryId]           TINYINT       NOT NULL,
    [City]                VARCHAR (50)  NOT NULL,
    [Address]             VARCHAR (50)  NOT NULL,
    [Telephone]           VARCHAR (20)  NOT NULL,
    [KeyCode]             VARCHAR (20)  NOT NULL,
    [CloudType]           TINYINT       NOT NULL,
    [AuthenticationType]  TINYINT       NOT NULL,
    [TimeStamp]           ROWVERSION    NOT NULL,
    CONSTRAINT [PK_Tenant] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tenant_Countries] FOREIGN KEY ([CountryId]) REFERENCES [Security].[Countries] ([Id])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena la información de tenant', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Tenant';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de la tabla', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Tenant', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre de tenant', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Tenant', @level2type = N'COLUMN', @level2name = N'Name';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado: 1|Solicitud de aprovisionamiento, 2|Tenant activo, 3|Tenant suspendido, 4|Tenant inactivo ', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Tenant', @level2type = N'COLUMN', @level2name = N'Status';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado: 1|Aprobacion jurídica, 2|Aprobación financiera, 3|Aprobación de operaciones', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Tenant', @level2type = N'COLUMN', @level2name = N'WorkFlowStatus';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Pais del tenant', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Tenant', @level2type = N'COLUMN', @level2name = N'CountryId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de nube, 1- Nube Publica, 2 - Nube Dedicada, 3 - Nube Privada, 4 - No Aplica', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Tenant', @level2type = N'COLUMN', @level2name = N'CloudType';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de autenticación: 1|Federada O365, 2|Federada Gmail, 3|Federada Amazon, 4|Federada Live.com, 5|Federada Linkedin, 6|B2C AD Indigo ', @level0type = N'SCHEMA', @level0name = N'Security', @level1type = N'TABLE', @level1name = N'Tenant', @level2type = N'COLUMN', @level2name = N'AuthenticationType';

