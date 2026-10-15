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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de notificación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHANOTIFICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correo electrónico institucional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CORREO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ubicación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'UBICACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Teléfono', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'TELEFONO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Dirección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DIRECCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Organización y área a la que pertenece', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ORGANIZACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Profesion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PROFESION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Informacion del reportante   Nombre ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de envió', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAENVIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Se ha enviado el dispositivo médico al Fabricante/ Importador/Distribuidor?  True = Si    False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ENVIADISPOSITIVO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '¿Dispositivo médico disponible para evaluación?  True = Si    False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DISPOMEDICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reportó al Fabricante/Distribuidor  True = Si      False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REPORFABRICANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Acciones correctivas y preventivas iniciadas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ACCICORRECTIVA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '960 Desgaste', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA960';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '950 Error de uso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA950';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '940 Capacidad de uso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA940';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '930 Sin definir', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA930';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '920 Trasporte y entrega', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA920';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '910 Entrenamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA910';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '900 Manipulación, falsificación, sabotaje', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA900';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '890 Condiciones de almacenamiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA890';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '880 Esterilización/desinfección/limpieza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA880';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '870 Software', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA870';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '860 Radiación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA860';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '850 Aseguramiento de la calidad en la institución para la atención de salud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA850';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '840 Medida de protección', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA840';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '830 Fuente de energía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA830';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '820 Condiciones del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA820';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '810 Anatomía/Fisiología del paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA810';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '800 Empaque', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA800';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '790 Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA790';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '780 No relacionado con el dispositivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA780';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '770 Condiciones no higiénicas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA770';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '760 Componentes Mecánicos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA760';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '750 Material', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA750';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '740 Fabricación', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA740';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '730 Mantenimiento', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA730';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '720 Escape/sellado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA720';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '710 Instrucciones para uso y rotulado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA710';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '700 Incompatibilidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA700';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '690 Ambiente inapropiado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA690';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '680 Falla en el dispositivo implantable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA680';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '670 Resultado falso de la prueba', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA670';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '660 Falso positivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA660';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '650 Falso negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA650';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '640 Fecha de expiración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA640';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '630 Interferencia Eletromagnética EIM', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA630';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '620 Contacto eléctrico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA620';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '610 Circuito eléctrico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA610';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '600 Componente eléctrico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA600';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '590 Desconexión', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA590';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '580 Diseño', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA580';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '570 Contaminación post-producción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA570';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '560 Contaminación durante la producción', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA560';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '550 Hadware de computador', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA550';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '540 Calibración', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA540';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '530 Uso de material biológico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA530';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '520 Falla en la alarma', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA520';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '510 Respuesta fisiologica anormal o inexplicable', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA510';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '500 Uso anoramal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CAUSA500';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Si señaló la opción Otro especifique ¿Cuál?', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'OTRODESENLACE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Desenlace del evento /incidente adverso  1= Muerte  2=Daño de una función o estructura corporal  3=Enfermedad o daño que amenace la vida  4=Requiere intervención médica o quirúrgica  5=incapacidad permanente parcial  6=Hospitalización o prolongación de la misa  7=Malformación congénita  8=No hubo daño  9=Otro', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESENLACEEVENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Descripción del evento o incidente adverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DESCRIPEVENTO';


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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha elaboracion del reporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAELABORACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha del evento /Incidente adverso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'FECHAEVENTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indique si el dispositivo médico ha sido utilizado más de una vez  True = si   False = No', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'DISPOSITIVOUTILI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Area de funcionamiento del dispositivo medico en el momento del evento/incidente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AREAUFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre o razon social del importador y/o distribuidor', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREIPORTADOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre o Razon social del fabricante', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREFABRICANTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Serial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'SERIAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Referencia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REFERENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Modelo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'MODELO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Lote', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'LOTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Registro sanitario o permiso de comercializacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'REGISTROSANITARIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre comercial del dispositivo Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBRECOMERCAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre generico del dispositivo Medico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREGENERICO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Diagnostico inicial del paciente ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Maturaleza  1 = Publica  2 =  Privada  3 = Mexta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NATURALEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nivel de Complejidad   1 = Baja  2 = Mediana  3 = Alta', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NIVELCOMPLEJI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nit del institucion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NIT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Ubicacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AUUBICACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Nombre de la institucion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'NOMBREINSTI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id del reporte que guarda igual que id del tabla CALREPORTE', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'IDCALREPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Autonumerico de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALTECNOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'ID';

