CREATE TABLE [dbo].[AGCONSULT] (
    [CODCENATE] CHAR (10)      NOT NULL,
    [CODIGOCON] CHAR (6)       NOT NULL,
    [DESCRICON] NVARCHAR (100) NOT NULL,
    [ESTADOCON] BIT            NOT NULL,
    [UFUCODIGO] CHAR (10)      NULL,
    [ID]        INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_AGCONSULT] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGCONSULT_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [IX_AGCONSULT] UNIQUE NONCLUSTERED ([CODIGOCON] ASC, [CODCENATE] ASC)
);


GO
ALTER TABLE [dbo].[AGCONSULT] NOCHECK CONSTRAINT [FK_AGCONSULT_ADCENATEN];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID o Autonumérico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de unidad Funcional ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Consultorio(Activo o Inactivo)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'ESTADOCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion del Consultorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'DESCRICON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Consultorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'CODIGOCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGCONSULT', @level2type = N'COLUMN', @level2name = N'CODCENATE';

