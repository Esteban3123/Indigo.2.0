CREATE TABLE [dbo].[IHFORMEDI_MeasurementUnit] (
    [Id]           INT          IDENTITY (1, 1) NOT NULL,
    [CODFORMED]    VARCHAR (20) NOT NULL,
    [CODUNIMED]    VARCHAR (20) NOT NULL,
    [CreationUser] VARCHAR (20) NOT NULL,
    [CreationDate] DATETIME     NOT NULL,
    CONSTRAINT [PK_IHFORMEDI_MeasurementUnit] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [fk_CODFORMED] FOREIGN KEY ([CODFORMED]) REFERENCES [dbo].[IHFORMEDI] ([CODFORMED]),
    CONSTRAINT [fk_CODUNIMED] FOREIGN KEY ([CODUNIMED]) REFERENCES [dbo].[INUNIMEDI] ([CODUNIMED])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Fecha de Creación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI_MeasurementUnit', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de usuario que realizo la creación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI_MeasurementUnit', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de la unidad de medida', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI_MeasurementUnit', @level2type = N'COLUMN', @level2name = N'CODUNIMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo de la forma farmaceutica  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI_MeasurementUnit', @level2type = N'COLUMN', @level2name = N'CODFORMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'IHFORMEDI_MeasurementUnit', @level2type = N'COLUMN', @level2name = N'Id';

