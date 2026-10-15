CREATE TABLE [EHR].[SchemesPathologies] (
    [Id]            INT      IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [SchemesId]     INT      NOT NULL,
    [PathologyCode] CHAR (4) NOT NULL,
    CONSTRAINT [PK_SchemeByPathologies] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_SchemesPathologies_Schemes] FOREIGN KEY ([SchemesId]) REFERENCES [EHR].[Schemes] ([Id]),
    CONSTRAINT [FK_SchemesPathologies_SchemesPathologies] FOREIGN KEY ([PathologyCode]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [EHR].[SchemesPathologies] NOCHECK CONSTRAINT [FK_SchemesPathologies_Schemes];


GO
ALTER TABLE [EHR].[SchemesPathologies] NOCHECK CONSTRAINT [FK_SchemesPathologies_SchemesPathologies];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del diagnostico - relacion (INDIAGNOS)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesPathologies', @level2type = N'COLUMN', @level2name = N'PathologyCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del esquema (EHR.Schemes)', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesPathologies', @level2type = N'COLUMN', @level2name = N'SchemesId';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'EHR', @level1type = N'TABLE', @level1name = N'SchemesPathologies', @level2type = N'COLUMN', @level2name = N'Id';

