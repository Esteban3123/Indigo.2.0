CREATE TABLE [Report].[TablaResultadosAntecedentes] (
    [NUMINGRES]       CHAR (10)      NOT NULL,
    [IPCODPACI]       VARCHAR (25)   NOT NULL,
    [IDHCHISPACA]     INT            NOT NULL,
    [CODANTECEDENTE]  INT            NOT NULL,
    [ID_VARIABLE]     INT            NOT NULL,
    [NOMBRE_VARIABLE] VARCHAR (150)  NOT NULL,
    [VALOR]           VARCHAR (5000) NOT NULL,
    [ID]              INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    CONSTRAINT [PK_TablaResultadosAntecedentes] PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TablaResultadosAntecedentes', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TablaResultadosAntecedentes', @level2type = N'COLUMN', @level2name = N'VALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el nombre de la variable', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TablaResultadosAntecedentes', @level2type = N'COLUMN', @level2name = N'NOMBRE_VARIABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el id variable', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TablaResultadosAntecedentes', @level2type = N'COLUMN', @level2name = N'ID_VARIABLE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del antecedente', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TablaResultadosAntecedentes', @level2type = N'COLUMN', @level2name = N'CODANTECEDENTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el id de la tabla HCHISPACA', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TablaResultadosAntecedentes', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del paciente', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TablaResultadosAntecedentes', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de ingreso', @level0type = N'SCHEMA', @level0name = N'Report', @level1type = N'TABLE', @level1name = N'TablaResultadosAntecedentes', @level2type = N'COLUMN', @level2name = N'NUMINGRES';

