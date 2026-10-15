CREATE TABLE [dbo].[HCORHEMCO] (
    [ID]           INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CODDIAGNO]    CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [HB]           VARCHAR (10)                                                                     NULL,
    [HTO]          VARCHAR (10)                                                                     NULL,
    [RCTOPLAQ]     VARCHAR (10)                                                                     NULL,
    [FIBRINOG]     VARCHAR (10)                                                                     NULL,
    [OTROSLAB]     VARCHAR (200)                                                                    NULL,
    [ITRESTAU]     BIT                                                                              NULL,
    [ITCAPTRA]     BIT                                                                              NULL,
    [ITVOLCIR]     BIT                                                                              NULL,
    [ITEXATRA]     BIT                                                                              NULL,
    [ITRESCIR]     BIT                                                                              NULL,
    [ITHEMOST]     BIT                                                                              NULL,
    [AUTOENVP]     BIT                                                                              NOT NULL,
    [PRIOTRAN]     TINYINT                                                                          NOT NULL,
    [RESERCIR]     BIT                                                                              NOT NULL,
    [TIPOCIRU]     VARCHAR (50)                                                                     NULL,
    [FECHACIR]     DATETIME                                                                         NULL,
    [TRANPREV]     BIT                                                                              NOT NULL,
    [REACTRAN]     BIT                                                                              NOT NULL,
    [RTHEMOLI]     BIT                                                                              NULL,
    [RTFEBRIL]     BIT                                                                              NULL,
    [RTURTICA]     BIT                                                                              NULL,
    [OTRASRET]     VARCHAR (200)                                                                    NULL,
    [AOEMBPRE]     BIT                                                                              NOT NULL,
    [AOEMBAC]      BIT                                                                              NULL,
    [AOERIFE]      BIT                                                                              NOT NULL,
    [IDETIPHIS]    CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]    NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]    CHAR (10)                                                                        NOT NULL,
    [CODCENATE]    CHAR (10)                                                                        NOT NULL,
    [FECORDMED]    DATETIME                                                                         NULL,
    [OBSERVACI]    VARCHAR (500)                                                                    NULL,
    [MANEXTPRO]    BIT                                                                              NOT NULL,
    [ESEMERGENCIA] BIT                                                                              NULL,
    [COOMBSDTO]    BIT                                                                              NULL,
    [RAI]          BIT                                                                              NULL,
    [AUTOCTRL]     BIT                                                                              NULL,
    [REARASANT]    BIT                                                                              NULL,
    [SOLEXTRAMU]   BIT                                                                              CONSTRAINT [DF_HCORHEMCO_SOLEXTRAMU] DEFAULT ((0)) NOT NULL,
    [HCCOMSANID]   INT                                                                              NULL,
    [INTERFAZ]     BIT                                                                              CONSTRAINT [DF_HCORHEMCO_INTERFAZ] DEFAULT ((0)) NOT NULL,
    [idAGASICITA]  INT                                                                              NULL,
    [SYNCMIRTH]    BIT                                                                              NULL,
    CONSTRAINT [PK_HCORHEMCO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCORHEMCO_ADCENATE] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCORHEMCO_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCORHEMCO_HCORHEMCO] FOREIGN KEY ([ID]) REFERENCES [dbo].[HCORHEMCO] ([ID]),
    CONSTRAINT [FK_HCORHEMCO_INDIAGNOS] FOREIGN KEY ([CODDIAGNO]) REFERENCES [dbo].[INDIAGNOS] ([CODDIAGNO]),
    CONSTRAINT [FK_HCORHEMCO_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_idAGASICITA_AGASICITA] FOREIGN KEY ([idAGASICITA]) REFERENCES [dbo].[AGASICITA] ([CODAUTONU])
);


GO
ALTER TABLE [dbo].[HCORHEMCO] NOCHECK CONSTRAINT [FK_HCORHEMCO_ADCENATE];


GO
ALTER TABLE [dbo].[HCORHEMCO] NOCHECK CONSTRAINT [FK_HCORHEMCO_ADINGRESO];


GO
ALTER TABLE [dbo].[HCORHEMCO] NOCHECK CONSTRAINT [FK_HCORHEMCO_HCORHEMCO];


GO
ALTER TABLE [dbo].[HCORHEMCO] NOCHECK CONSTRAINT [FK_HCORHEMCO_INDIAGNOS];


GO
ALTER TABLE [dbo].[HCORHEMCO] NOCHECK CONSTRAINT [FK_HCORHEMCO_INPACIENT];


GO
ALTER TABLE [dbo].[HCORHEMCO] NOCHECK CONSTRAINT [FK_idAGASICITA_AGASICITA];


GO
CREATE NONCLUSTERED INDEX [IX_HCORHEMCO_IPCODPACI_NUMEFOLIO_NUMINGRES_IDETIPHIS]
    ON [dbo].[HCORHEMCO]([IPCODPACI] ASC, [NUMEFOLIO] ASC, [NUMINGRES] ASC, [IDETIPHIS] ASC)
    INCLUDE([MANEXTPRO]);


GO
CREATE NONCLUSTERED INDEX [IX_HCORHEMCO__IPCODPACI]
    ON [dbo].[HCORHEMCO]([IPCODPACI] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identifica si guarda la conexión de miRConnect', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'SYNCMIRTH';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Relación con la tabla AGASICITA', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'idAGASICITA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Determina si se envio o no a la interfaz hemocomponentes.  1 - si envio  0 - no envio  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'INTERFAZ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tiene relación con la tabla HCCOMSAN', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'HCCOMSANID';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Identifica cuando la solicitud es extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'SOLEXTRAMU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si se realizó rastreo de anticuerpos, 1-> si, 0-> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'REARASANT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Auto Control 1-> positivo, 0->negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'AUTOCTRL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RAI 1->Positivo, 0-> Negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RAI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'COOMBS DTO 1-> Positivo, 0-> Negativo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'COOMBSDTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica si es de emergencia al momento de hacer reserva', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ESEMERGENCIA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Aplica para manejo externo o extramural', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'MANEXTPRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Registro de la solicitud de hemocomponente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'FECORDMED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo Centro de Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Ingreso', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de historia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'IDETIPHIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Obstetricos: Eritroblasis Fetal  1 = si  0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'AOERIFE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Obstetricos: Embarazo Actual  1 = si  0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'AOEMBAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Antecedentes Obstetricos: Embarazos Previos  1 = si  0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'AOEMBPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otras Reacciones Transfusionales', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'OTRASRET';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Urticariantes  1 = si  0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RTURTICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Febriles   1 = si  0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RTFEBRIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Hemoliticas  1 = si  0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RTHEMOLI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Reacciones Transfusionales:  1 = si  0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'REACTRAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Transfusiones Previas:  1 = si  0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'TRANPREV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de Cirugía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'FECHACIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Cirugía', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'TIPOCIRU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Rerseva para cirugía  1 = si  0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RESERCIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Prioridad para la transfusion:  1. Emergencia (hasta 15 min)  2.  Urgencia (hasta 1 hora)  3.  Urgencia Diferida (hasta 3 horas)  4. Normal (hasta 24 horas)  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'PRIOTRAN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Por la urgencia de la solicitud autoriza enviar el (los) producto(s) solicitado(s) sin pruebas cruzadas  1 = si  0 = no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'AUTOENVP';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Opcion Indicaciones para la Transfucion:   Hemostasis 1-> si, 0-> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITHEMOST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Opcion Indicaciones para la Transfucion:   Reserva para Cirugía 1-> si, 0-> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITRESCIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Opciones Indicaciones para la Transfucion:   Exaguineo Transfusion 1-> si, 0-> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITEXATRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Opciones Indicaciones para la Transfucion:   Volumen Circulatorio 1-> si, 0-> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITVOLCIR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Indicaciones para la Transfucion:   Capacidad Transportadora 1-> si, 0-> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITCAPTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Opcion Indicaciones para la Transfucion:   Restaurar o Mantener. 1-> si, 0-> no', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ITRESTAU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Otros Laboratorios', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'OTROSLAB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fibrinogeno', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'FIBRINOG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'RCTO Plaquetas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'RCTOPLAQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'HTO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'HTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'HB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'HB';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del diagnostico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'CODDIAGNO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'ID de la tabla de hemocomponentes', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCORHEMCO', @level2type = N'COLUMN', @level2name = N'ID';

