CREATE TABLE [Integrations].[FomagAssurance] (
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    [CodeIndigo]      VARCHAR (50)  NOT NULL,
    [CodeIntegration] VARCHAR (50)  NOT NULL,
    [Description]     VARCHAR (250) NOT NULL,
    [URLAz]           VARCHAR (250) NULL,
    CONSTRAINT [PK_FomagAssurance] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'URL de la azure function de validacion de derechos', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance', @level2type = N'COLUMN', @level2name = N'URLAz';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripcion de la aseguradora', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance', @level2type = N'COLUMN', @level2name = N'Description';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de la entidad del lado del Fomag', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance', @level2type = N'COLUMN', @level2name = N'CodeIntegration';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo de indigo , es el code de contrach.HealthAdministrator', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance', @level2type = N'COLUMN', @level2name = N'CodeIndigo';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'tabla de integracion para guardad las entidades que aplicacion integracion fomag - jersalud', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'FomagAssurance';

