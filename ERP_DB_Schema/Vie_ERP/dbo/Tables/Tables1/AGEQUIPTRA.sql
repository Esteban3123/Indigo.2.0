CREATE TABLE [dbo].[AGEQUIPTRA] (
    [ID]        INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODEQUIPO] VARCHAR (15)  NOT NULL,
    [CODCENATE] CHAR (10)     NOT NULL,
    [TIPOEQUIP] TINYINT       NOT NULL,
    [ESTEQUIP]  BIT           NOT NULL,
    [DESCREQUI] VARCHAR (100) NOT NULL,
    [CODINVI]   VARCHAR (20)  NULL,
    [CONDESPE]  BIT           NULL,
    [OBSERVAC]  VARCHAR (500) NULL,
    [COLOR]     NVARCHAR (50) NULL,
    CONSTRAINT [PK_AGEQUIPTRA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AGEQUIPTRA_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [IX_AGEQUIPTRA] UNIQUE NONCLUSTERED ([CODCENATE] ASC, [CODEQUIPO] ASC)
);


GO
ALTER TABLE [dbo].[AGEQUIPTRA] NOCHECK CONSTRAINT [FK_AGEQUIPTRA_ADCENATEN];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Color del equipo de tratamiento que se registra si el equipo es marcado como condiciones especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'COLOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observacion que se registra si el equipo es para pacientes con condiciones especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'OBSERVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Condicion Especial: Determina si el equipo estrictamente Aplica a pacientes con tratamientos especiales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'CONDESPE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Invima del equipo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'CODINVI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'descripcion del equipo de  tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'DESCREQUI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del equipo d etratamiento  1-> ACTIVO   0 ->INACTIVO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'ESTEQUIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Equipo 1 -> Quimio 2-> Radio 3-> Dialisis', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'TIPOEQUIP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'codigo de relacion con centros de atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de equipo de tratamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'CODEQUIPO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla de registro de equipos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AGEQUIPTRA', @level2type = N'COLUMN', @level2name = N'ID';

