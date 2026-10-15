CREATE TABLE [dbo].[HCPARINQX] (
    [CODCENATE]       CHAR (10) NOT NULL,
    [CODSERIPS]       CHAR (20) NOT NULL,
    [TRATAEGRE]       CHAR (1)  NOT NULL,
    [ID]              INT       IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [USUARIOCREACION] CHAR (20) NULL,
    [FECHACREACION]   DATETIME  NULL,
    CONSTRAINT [PK_HCPARINQX] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCPARINQX_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCPARINQX_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCPARINQX_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [IX_HCPARINQX] UNIQUE NONCLUSTERED ([CODCENATE] ASC, [CODSERIPS] ASC)
);


GO
ALTER TABLE [dbo].[HCPARINQX] NOCHECK CONSTRAINT [FK_HCPARINQX_ADCENATEN];


GO
ALTER TABLE [dbo].[HCPARINQX] NOCHECK CONSTRAINT [FK_HCPARINQX_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCPARINQX] NOCHECK CONSTRAINT [FK_HCPARINQX_SEGusuaru_1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID o Autonumérico de la Tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'ID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Al Egreso Marcar como:  1. Intrahospitalario  2. Extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'TRATAEGRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Unico de Procedimientos y Servicios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'CODSERIPS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCPARINQX', @level2type = N'COLUMN', @level2name = N'CODCENATE';

