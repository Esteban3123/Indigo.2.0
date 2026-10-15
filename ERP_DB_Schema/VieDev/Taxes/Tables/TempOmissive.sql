CREATE TABLE [Taxes].[TempOmissive] (
    [Id]  INT          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [Nit] VARCHAR (50) NOT NULL
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Almacena el nil del registro omisivo.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TempOmissive', @level2type = N'COLUMN', @level2name = N'Nit';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único del registro.', @level0type = N'SCHEMA', @level0name = N'Taxes', @level1type = N'TABLE', @level1name = N'TempOmissive', @level2type = N'COLUMN', @level2name = N'Id';

