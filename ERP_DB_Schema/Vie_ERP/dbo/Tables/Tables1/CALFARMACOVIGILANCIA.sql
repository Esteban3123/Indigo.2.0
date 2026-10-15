CREATE TABLE [dbo].[CALFARMACOVIGILANCIA] (
    [ID]                  INT                                                                            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCALREPORTE]        INT                                                                            NOT NULL,
    [FECHANACIMIENTO]     DATE MASKED WITH (FUNCTION = 'default()')                                      NOT NULL,
    [ORIGENREPORTE]       CHAR (20)                                                                      NOT NULL,
    [NOMBREINSTITUCION]   VARCHAR (100)                                                                  NOT NULL,
    [CODIGOPNF]           VARCHAR (15)                                                                   NOT NULL,
    [NOMBREREPORTANTE]    VARCHAR (100)                                                                  NOT NULL,
    [PROFESIONREPORTANTE] CHAR (4)                                                                       NOT NULL,
    [CORREOREPORTANTE]    VARCHAR (100)                                                                  NOT NULL,
    [PESO]                NUMERIC (18, 3)                                                                NOT NULL,
    [TALLA]               INT                                                                            NOT NULL,
    [CODDIAGNO]           CHAR (4)                                                                       NOT NULL,
    [TITULARREGISTRO]     VARCHAR (100)                                                                  NOT NULL,
    [NOMBRECOMERCIAL]     VARCHAR (100)                                                                  NOT NULL,
    [REGISTROSANITARIO]   VARCHAR (100)                                                                  NULL,
    [LOTE]                VARCHAR (15)                                                                   NULL,
    [FECHAINICIOEVENTO]   DATE                                                                           NOT NULL,
    [DESCRIPCION]         VARCHAR (MAX) MASKED WITH (FUNCTION = 'partial(0, "Description_Ofuscado", 0)') NOT NULL,
    [DESRECUPSINSECUELAS] BIT                                                                            NULL,
    [DESRECUPCONSECUELAS] BIT                                                                            NULL,
    [DESRECUPRESOLVIENDO] BIT                                                                            NULL,
    [DESNORECUPERADO]     BIT                                                                            NULL,
    [DESFATAL]            BIT                                                                            NULL,
    [DESDESCONOCIDO]      BIT                                                                            NULL,
    [SERIPRODUJO]         BIT                                                                            NULL,
    [SERIADNOMALIA]       BIT                                                                            NULL,
    [AMENAZAVIDA]         BIT                                                                            NULL,
    [SERIAMENAZAMUERTE]   BIT                                                                            NULL,
    [SERIPRODUJODISCAPAC] BIT                                                                            NULL,
    [EVENTODESPUES]       INT                                                                            NOT NULL,
    [OTROSFACTORES]       INT                                                                            NOT NULL,
    [EVENTODESAPARE]      INT                                                                            NOT NULL,
    [PACIENTEMISMAREACC]  INT                                                                            NOT NULL,
    [AMPLIARINFORMAC]     INT                                                                            NOT NULL,
    [FECHAMUERTE]         DATE                                                                           NULL,
    CONSTRAINT [PK_CALFARMACOVIGILANCIA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALFARMACOVIGILANCIA_ADACTIVID] FOREIGN KEY ([PROFESIONREPORTANTE]) REFERENCES [dbo].[ADACTIVID] ([codactivi]),
    CONSTRAINT [FK_CALFARMACOVIGILANCIA_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO])
);


GO
ALTER TABLE [dbo].[CALFARMACOVIGILANCIA] NOCHECK CONSTRAINT [FK_CALFARMACOVIGILANCIA_ADACTIVID];


GO
ALTER TABLE [dbo].[CALFARMACOVIGILANCIA] NOCHECK CONSTRAINT [FK_CALFARMACOVIGILANCIA_INDIAGNOS];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de la muerte ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAMUERTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1= Si   2= No   3= NO sabe ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AMPLIARINFORMAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1= Si   2= No   3= NO sabe ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PACIENTEMISMAREACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1= Si   2= No   3= NO sabe ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'EVENTODESAPARE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1= Si   2= No   3= NO sabe ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'OTROSFACTORES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1= Si   2= No   3= NO sabe ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'EVENTODESPUES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Seriedad Produjo discapacidad o incapacidad permanente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERIPRODUJODISCAPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Seriedad Muerte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERIAMENAZAMUERTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Seriedad amenaza de vida ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AMENAZAVIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Seriedad Anomalia congenita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERIADNOMALIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Seriedad Produjo o prolongo hospitalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERIPRODUJO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desenlace del evento Desconocido', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESDESCONOCIDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desenlace del evento Fatal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESFATAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desenlace del evento No recuperado / No resuelto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESNORECUPERADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desenlace del evento Recuperado / Resolviendo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESRECUPRESOLVIENDO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desenlace del evento Recuperado / Resuelto Con secuelas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESRECUPCONSECUELAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desenlace del evento Recuperado / Resuelto sin secuelas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESRECUPSINSECUELAS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripcion y analisis del evento adverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESCRIPCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de inico del evento adverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAINICIOEVENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lote ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'LOTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro sanitario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REGISTROSANITARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre comercial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRECOMERCIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Titular del registro sanitario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TITULARREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo diagnostico principal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Talla en CM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TALLA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Peso en KG', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PESO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo electronico institucional del reportante primario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CORREOREPORTANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesion del reportante primario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROFESIONREPORTANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre del reportante primario', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREREPORTANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo PNF', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CODIGOPNF';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la institucion donde ocurrio el evento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREINSTITUCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Origen del Reporte ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ORIGENREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de nacimiento  del paciente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHANACIMIENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del Reporte ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ID';

