CREATE TABLE [dbo].[RIASCUPSD] (
    [ID]               INT     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDRIASCUPS]       INT     NOT NULL,
    [REGLA]            INT     NOT NULL,
    [EDADMINIMA]       INT     NULL,
    [EDADMAXIMA]       INT     NULL,
    [UNIDADRANGO]      INT     NULL,
    [FRECUENCIA]       INT     NULL,
    [UNIDADFRECUENCIA] INT     NULL,
    [REQUIEREORDENMED] BIT     NOT NULL,
    [NUMEROVECESORDEN] INT     NULL,
    [CANTIDADPERIODO]  INT     NULL,
    [SEXO]             TINYINT CONSTRAINT [DF_RIASCUPSD_SEXO] DEFAULT ((2)) NOT NULL,
    [ESTADO]           BIT     CONSTRAINT [DF_RIASCUPSD_ESTADO] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_RIASCUPSD] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Define el esatdo de la regla:  1 - Activo   0 - inactivo   ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'ESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina a que sexo se aplica el rango:  0 - Masculino  1 - Femenino  2 - Ambos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'SEXO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Solo aplica para la regla (3 -Frecuencia por ultima fecha de realización) y para poder especificar:  1 cada X Tiempo, Así:  Ejemplo   1 cada 3 Años  1 cada 3 Semestres  1 cada 3 Trimestres  1 cada 3 Meses', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'CANTIDADPERIODO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda Requiere de orden medica Si = 1  y No = 0', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'REQUIEREORDENMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Depende del campo REGLA     Unicamente cuando la Regla es  ( 3 -Frecuencia por ultima fecha de realización) se debe diligenciar este campo  para este tipo de item la frecuencia por defecto es 1 (uno)  1 - 1 Cada x año    2 - 1 Cada x semestre  3 - 1 Cada x trimestre  4 - 1 Cada x mes    la x la define el campo (CANTIDADPERIODO)    cuando la regla es  ( 4 -Frecuencia por periodo vigente)    5 - En el año vigente  6 - En el semestre vigente  7 - En el trimestre vigente  8 - En el mes vigente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'UNIDADFRECUENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Unidad de Rango:  1 - Dias  2 - Meses  3 - Años', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'UNIDADRANGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Sin Regla: No habilita los campos de frecuencia  2 - Frecuencia por rango de edad  3 - Frecuencia por ultima fecha de realización  4 - Frecuencia por periodo vigente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'RIASCUPSD', @level2type = N'COLUMN', @level2name = N'REGLA';

