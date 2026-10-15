CREATE TABLE [Integrations].[HomologationSpecialties] (
    [CodeIntegration] VARCHAR (50)  NULL,
    [NameIntegration] VARCHAR (200) NULL,
    [CodeIndigo]      VARCHAR (50)  NULL,
    [NameIndigo]      VARCHAR (200) NULL,
    [Observacion]     VARCHAR (500) NULL,
    [Id]              INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_HomologationSpecialties] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'tabla homologacion especialidades indigo con FOMAG', @level0type = N'SCHEMA', @level0name = N'Integrations', @level1type = N'TABLE', @level1name = N'HomologationSpecialties';

