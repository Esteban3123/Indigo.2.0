CREATE TABLE [dbo].[HCCRIUNID] (
    [CODCONCEC]           NUMERIC (18) IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODTIPCRI]           CHAR (5)     NOT NULL,
    [DESTIPCRI]           CHAR (1000)  NOT NULL,
    [UFUTIPUNI]           INT          NOT NULL,
    [INDAUDFOR]           NUMERIC (18) NOT NULL,
    [CODTIPEST]           CHAR (3)     NULL,
    [TIPCRITERIO]         BIT          NULL,
    [USUARIOCREACION]     CHAR (20)    NULL,
    [FECHACREACION]       DATETIME     NULL,
    [USUARIOMODIFICACION] CHAR (20)    NULL,
    [FECHAMODIFICACION]   DATETIME     NULL,
    [Neonate]             BIT          NULL,
    CONSTRAINT [PK_HCCRIUNID_1] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_HCCRIUNID_SEGusuaru_1] FOREIGN KEY ([USUARIOCREACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI]),
    CONSTRAINT [FK_HCCRIUNID_SEGusuaru_2] FOREIGN KEY ([USUARIOMODIFICACION]) REFERENCES [dbo].[SEGusuaru] ([CODUSUARI])
);


GO
ALTER TABLE [dbo].[HCCRIUNID] NOCHECK CONSTRAINT [FK_HCCRIUNID_SEGusuaru_1];


GO
ALTER TABLE [dbo].[HCCRIUNID] NOCHECK CONSTRAINT [FK_HCCRIUNID_SEGusuaru_2];


GO
CREATE NONCLUSTERED INDEX [IX_HCCRIUNID__CODTIPCRI]
    ON [dbo].[HCCRIUNID]([CODTIPCRI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Almacena el si la opcion de neonato del criterio de ingreso esta activa o no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'Neonate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'FECHAMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Modificación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'USUARIOMODIFICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'FECHACREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Usuario Creación del Registro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'USUARIOCREACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Campo para conocer tipo de criterio 1-> (Ingreso,Estancia)  2-> (Egreso)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'TIPCRITERIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Tipo de Estancia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'CODTIPEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo de Auditoria', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Unidad Funcional:  1: Urgencias  2: Hospitalizacion  3: Apoyo Dx  4: Apoyo Terapeutico  5: Unidades de Cuidado Intensivo Adulto  6: Unidades de Cuidado Intermedio Adulto  7: Unidades de Cuidado Intensivo Pediatrica  8: Unidades de Cuidado Intermedio Pediatrica  9: Unidades de Cuidado Intensivo Neonatal  10: Unidades de Cuidado Intermedio Neonatal  11: Unidades de Cuidado Basico Neonatal  12: Unidad Renal  13 Unidad Oncologica  14: Unidad Medicina Nuclear  15: Consulta Externa  16: Unidad Mental  17: Unidad de Quemados  18: Unidad de Cuidado Paliativo  19: Cirugia  20: Laboratorio  21: Cardiologia No Invasiva  22: Cardiologia Invasiva  23: Gineco-Obstetricia  24: Otras', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'UFUTIPUNI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion Tipos de Criterios Ingreso a Unidades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'DESTIPCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Tipo de Critrios Ingreso a Unidades', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'CODTIPCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Consecutivo de la Tabla ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCCRIUNID', @level2type = N'COLUMN', @level2name = N'CODCONCEC';

