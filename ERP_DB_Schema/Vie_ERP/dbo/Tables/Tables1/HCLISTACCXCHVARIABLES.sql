CREATE TABLE [dbo].[HCLISTACCXCHVARIABLES] (
    [ID]            INT IDENTITY (1, 1) NOT NULL,
    [IDHCLISTACC]   INT NOT NULL,
    [IDCHVARIABLES] INT NOT NULL,
    [OBLIGATORIO]   BIT NOT NULL,
    [ESTADO]        BIT NOT NULL,
    CONSTRAINT [PK_HCLISTACCXCHVARIABLES] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCLISTACCXCHVARIABLES_CHVARIABLES] FOREIGN KEY ([IDCHVARIABLES]) REFERENCES [dbo].[CHVARIABLES] ([ID]),
    CONSTRAINT [FK_HCLISTACCXCHVARIABLES_HCLISTACC] FOREIGN KEY ([IDHCLISTACC]) REFERENCES [dbo].[HCLISTACC] ([CODCONSEC])
);


GO
ALTER TABLE [dbo].[HCLISTACCXCHVARIABLES] NOCHECK CONSTRAINT [FK_HCLISTACCXCHVARIABLES_CHVARIABLES];


GO
ALTER TABLE [dbo].[HCLISTACCXCHVARIABLES] NOCHECK CONSTRAINT [FK_HCLISTACCXCHVARIABLES_HCLISTACC];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el estado 1 = true = Activo    0 = false = Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda si es obligatorio 1 = si  0 = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES', @level2type = N'COLUMN', @level2name = N'OBLIGATORIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'id variables lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES', @level2type = N'COLUMN', @level2name = N'IDCHVARIABLES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'ID cabecera lista de chequeo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES', @level2type = N'COLUMN', @level2name = N'IDHCLISTACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCLISTACCXCHVARIABLES', @level2type = N'COLUMN', @level2name = N'ID';

