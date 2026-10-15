CREATE TABLE [dbo].[CALTECNOVIGILANCIA] (
    [ID]                   INT                                                                          IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCALREPORTE]         INT                                                                          NOT NULL,
    [NOMBREINSTI]          VARCHAR (100)                                                                NOT NULL,
    [AUUBICACI]            CHAR (20)                                                                    NOT NULL,
    [NIT]                  VARCHAR (15)                                                                 NOT NULL,
    [NIVELCOMPLEJI]        INT                                                                          NULL,
    [NATURALEZA]           INT                                                                          NOT NULL,
    [CODDIAGNO]            CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)') NOT NULL,
    [NOMBREGENERICO]       VARCHAR (100)                                                                NOT NULL,
    [NOMBRECOMERCAIL]      VARCHAR (100)                                                                NOT NULL,
    [REGISTROSANITARIO]    VARCHAR (100)                                                                NULL,
    [LOTE]                 VARCHAR (15)                                                                 NULL,
    [MODELO]               VARCHAR (15)                                                                 NULL,
    [REFERENCIA]           VARCHAR (15)                                                                 NULL,
    [SERIAL]               VARCHAR (15)                                                                 NULL,
    [NOMBREFABRICANTE]     VARCHAR (100)                                                                NOT NULL,
    [NOMBREIPORTADOR]      VARCHAR (100)                                                                NULL,
    [AREAUFUCODIGO]        CHAR (10)                                                                    NULL,
    [DISPOSITIVOUTILI]     BIT                                                                          NULL,
    [FECHAEVENTO]          DATE                                                                         NOT NULL,
    [FECHAELABORACION]     DATE                                                                         NOT NULL,
    [DETENCIONANTES]       BIT                                                                          NULL,
    [DETENCIONDURANTE]     BIT                                                                          NULL,
    [DETENCIONDESPUES]     BIT                                                                          NULL,
    [CLASIEVENADVESERIO]   BIT                                                                          NULL,
    [CLASIEVENADVNOESERIO] BIT                                                                          NULL,
    [CLASIINCIADVESERIO]   BIT                                                                          NULL,
    [CLASIINCIADVENOSERIO] BIT                                                                          NULL,
    [DESCRIPEVENTO]        VARCHAR (MAX)                                                                NOT NULL,
    [DESENLACEEVENTO]      INT                                                                          NOT NULL,
    [OTRODESENLACE]        VARCHAR (100)                                                                NULL,
    [CAUSA500]             BIT                                                                          NULL,
    [CAUSA510]             BIT                                                                          NULL,
    [CAUSA520]             BIT                                                                          NULL,
    [CAUSA530]             BIT                                                                          NULL,
    [CAUSA540]             BIT                                                                          NULL,
    [CAUSA550]             BIT                                                                          NULL,
    [CAUSA560]             BIT                                                                          NULL,
    [CAUSA570]             BIT                                                                          NULL,
    [CAUSA580]             BIT                                                                          NULL,
    [CAUSA590]             BIT                                                                          NULL,
    [CAUSA600]             BIT                                                                          NULL,
    [CAUSA610]             BIT                                                                          NULL,
    [CAUSA620]             BIT                                                                          NULL,
    [CAUSA630]             BIT                                                                          NULL,
    [CAUSA640]             BIT                                                                          NULL,
    [CAUSA650]             BIT                                                                          NULL,
    [CAUSA660]             BIT                                                                          NULL,
    [CAUSA670]             BIT                                                                          NULL,
    [CAUSA680]             BIT                                                                          NULL,
    [CAUSA690]             BIT                                                                          NULL,
    [CAUSA700]             BIT                                                                          NULL,
    [CAUSA710]             BIT                                                                          NULL,
    [CAUSA720]             BIT                                                                          NULL,
    [CAUSA730]             BIT                                                                          NULL,
    [CAUSA740]             BIT                                                                          NULL,
    [CAUSA750]             BIT                                                                          NULL,
    [CAUSA760]             BIT                                                                          NULL,
    [CAUSA770]             BIT                                                                          NULL,
    [CAUSA780]             BIT                                                                          NULL,
    [CAUSA790]             BIT                                                                          NULL,
    [CAUSA800]             BIT                                                                          NULL,
    [CAUSA810]             BIT                                                                          NULL,
    [CAUSA820]             BIT                                                                          NULL,
    [CAUSA830]             BIT                                                                          NULL,
    [CAUSA840]             BIT                                                                          NULL,
    [CAUSA850]             BIT                                                                          NULL,
    [CAUSA860]             BIT                                                                          NULL,
    [CAUSA870]             BIT                                                                          NULL,
    [CAUSA880]             BIT                                                                          NULL,
    [CAUSA890]             BIT                                                                          NULL,
    [CAUSA900]             BIT                                                                          NULL,
    [CAUSA910]             BIT                                                                          NULL,
    [CAUSA920]             BIT                                                                          NULL,
    [CAUSA930]             BIT                                                                          NULL,
    [CAUSA940]             BIT                                                                          NULL,
    [CAUSA950]             BIT                                                                          NULL,
    [CAUSA960]             BIT                                                                          NULL,
    [ACCICORRECTIVA]       VARCHAR (MAX)                                                                NULL,
    [REPORFABRICANTE]      BIT                                                                          NULL,
    [FECHAREPORTE]         DATE                                                                         NULL,
    [DISPOMEDICO]          BIT                                                                          NULL,
    [ENVIADISPOSITIVO]     BIT                                                                          NULL,
    [FECHAENVIO]           DATE                                                                         NULL,
    [NOMBRE]               VARCHAR (100)                                                                NULL,
    [PROFESION]            CHAR (4)                                                                     NULL,
    [ORGANIZACION]         CHAR (10)                                                                    NULL,
    [DIRECCION]            VARCHAR (100)                                                                NULL,
    [TELEFONO]             VARCHAR (15)                                                                 NULL,
    [UBICACION]            CHAR (20)                                                                    NULL,
    [CORREO]               VARCHAR (100)                                                                NULL,
    [FECHANOTIFICA]        DATE                                                                         NULL,
    [AUTORIZA]             BIT                                                                          NULL,
    CONSTRAINT [PK_CALTECNOVIGILANCIA] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CALTECNOVIGILANCIA_CALREPORTE] FOREIGN KEY ([IDCALREPORTE]) REFERENCES [dbo].[CALREPORTE] ([ID]),
    CONSTRAINT [FK_CALTECNOVIGILANCIA_CALTECNOVIGILANCIA] FOREIGN KEY ([UBICACION]) REFERENCES [dbo].[INUBICACI] ([AUUBICACI]),
    CONSTRAINT [FK_CALTECNOVIGILANCIA_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_CALTECNOVIGILANCIA_INUBICACI] FOREIGN KEY ([AUUBICACI]) REFERENCES [dbo].[INUBICACI] ([AUUBICACI]),
    CONSTRAINT [FK_CALTECNOVIGILANCIA_INUNIFUNC] FOREIGN KEY ([ORGANIZACION]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_CALTECNOVIGILANCIA_INUNIFUNC1] FOREIGN KEY ([AREAUFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[CALTECNOVIGILANCIA] NOCHECK CONSTRAINT [FK_CALTECNOVIGILANCIA_CALREPORTE];


GO
ALTER TABLE [dbo].[CALTECNOVIGILANCIA] NOCHECK CONSTRAINT [FK_CALTECNOVIGILANCIA_CALTECNOVIGILANCIA];


GO
ALTER TABLE [dbo].[CALTECNOVIGILANCIA] NOCHECK CONSTRAINT [FK_CALTECNOVIGILANCIA_INDIAGNOS];


GO
ALTER TABLE [dbo].[CALTECNOVIGILANCIA] NOCHECK CONSTRAINT [FK_CALTECNOVIGILANCIA_INUBICACI];


GO
ALTER TABLE [dbo].[CALTECNOVIGILANCIA] NOCHECK CONSTRAINT [FK_CALTECNOVIGILANCIA_INUNIFUNC];


GO
ALTER TABLE [dbo].[CALTECNOVIGILANCIA] NOCHECK CONSTRAINT [FK_CALTECNOVIGILANCIA_INUNIFUNC1];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autoriza la divulgación de la información y origen del reporte al fabricante  True = Si     False = NO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AUTORIZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Informacion del reportante   Nombre ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Se ha enviado el dispositivo médico al Fabricante/ Importador/Distribuidor?  True = Si    False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ENVIADISPOSITIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Dispositivo médico disponible para evaluación?  True = Si    False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DISPOMEDICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reportó al Fabricante/Distribuidor  True = Si      False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REPORFABRICANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desenlace del evento /incidente adverso  1= Muerte  2=Daño de una función o estructura corporal  3=Enfermedad o daño que amenace la vida  4=Requiere intervención médica o quirúrgica  5=incapacidad permanente parcial  6=Hospitalización o prolongación de la misa  7=Malformación congénita  8=No hubo daño  9=Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESENLACEEVENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación    Incidente adverso no serio = True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIINCIADVENOSERIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación  Incidente adverso serio = True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIINCIADVESERIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación  Evento adverso no serio = True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIEVENADVNOESERIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Clasificación  Evento adverso serio = True = Seleccionado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CLASIEVENADVESERIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detección del evento/incidente adverso  Despues del uso del DM = True = Seleccionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETENCIONDESPUES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detección del evento/incidente adverso  Durante del uso del DM = True = Seleccionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETENCIONDURANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Detección del evento/incidente adverso  Antes del uso del DM = True = Seleccionado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DETENCIONANTES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indique si el dispositivo médico ha sido utilizado más de una vez  True = si   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DISPOSITIVOUTILI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Maturaleza  1 = Publica  2 =  Privada  3 = Mexta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NATURALEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel de Complejidad   1 = Baja  2 = Mediana  3 = Alta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NIVELCOMPLEJI';

