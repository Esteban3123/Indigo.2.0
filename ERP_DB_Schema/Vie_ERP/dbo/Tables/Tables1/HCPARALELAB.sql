CREATE TABLE [dbo].[HCPARALELAB] (
    [CODCENATE]           CHAR (10) NOT NULL,
    [CODSERIPS]           CHAR (20) NOT NULL,
    [LABRECMUE]           TINYINT   NULL,
    [UNITIEREC]           TINYINT   NULL,
    [LABRECMUE2]          TINYINT   NULL,
    [UNITIEREC2]          TINYINT   NULL,
    [LABENTRES]           TINYINT   NULL,
    [UNITIEENT]           TINYINT   NULL,
    [LABENTRES2]          TINYINT   NULL,
    [UNITIEENT2]          TINYINT   NULL,
    [ID]                  INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [USUARIOCREACION]     CHAR (20) NULL,
    [FECHACREACION]       DATETIME  NULL,
    [USUARIOMODIFICACION] CHAR (20) NULL,
    [FECHAMODIFICACION]   DATETIME  NULL,
    CONSTRAINT [PK_HCPARALELAB] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARALELAB_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARALELAB_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCPARALELAB_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_HCPARALELAB_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [IX_HCPARALELAB] UNIQUE NONCLUSTERED ([CODCENATE] ASC, [CODSERIPS] ASC)
);


GO
ALTER TABLE [dbo].[HCPARALELAB] NOCHECK CONSTRAINT [FK_HCPARALELAB_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPARALELAB] NOCHECK CONSTRAINT [FK_HCPARALELAB_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCPARALELAB] NOCHECK CONSTRAINT [FK_HCPARALELAB_SEGusuaru_1];


GO
ALTER TABLE [dbo].[HCPARALELAB] NOCHECK CONSTRAINT [FK_HCPARALELAB_SEGusuaru_2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID o Autonumérico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Laboratorios - Unidad de Tiempo Maximo de Entrega de Resultados 2 Instancia (1-Minutos,2-Horas,3-Dias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'UNITIEENT2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Laboratorios - Tiempo Maximo de Entrega de Resultados 2 Instancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'LABENTRES2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Laboratorios - Unidad de Tiempo Maximo de Entrega de Resultados (1-Minutos,2-Horas,3-Dias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'UNITIEENT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Laboratorios - Tiempo Maximo de Entrega de Resultados', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'LABENTRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Laboratorios - Unidad de Tiempo Maximo de Recoleccion de la Muestra 2 Instancia (1-Minutos,2-Horas,3-Dias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'UNITIEREC2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Laboratorios - Tiempo Maximo de Recoleccion de la Muestra 2 Instancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'LABRECMUE2';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Laboratorios - Unidad de Tiempo Maximo de Recoleccion de la Muestra(1-Minutos,2-Horas,3-Dias)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'UNITIEREC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Laboratorios - Tiempo Maximo de Recoleccion de la Muestra', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'LABRECMUE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del centro de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARALELAB', @level2type = N'COLUMN', @level2name = N'CODCENATE';

