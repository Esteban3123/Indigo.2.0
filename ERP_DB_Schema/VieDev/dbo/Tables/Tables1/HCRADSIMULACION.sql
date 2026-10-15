CREATE TABLE [dbo].[HCRADSIMULACION] (
    [ID]                  INT           IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDHCRADORDEN]        INT           NOT NULL,
    [PROTOCOLO]           INT           NOT NULL,
    [OTROPROTOCOLO]       VARCHAR (100) NULL,
    [SOPPOPLITEO]         BIT           NULL,
    [SOPDUALLEG]          BIT           NULL,
    [SOPOVERLAY]          BIT           NULL,
    [SOPCARAPRONO]        BIT           NULL,
    [SOPCOJIN]            BIT           NULL,
    [SOPWING]             BIT           NULL,
    [SOPRETRACTOR]        BIT           NULL,
    [SOPHOLDER]           BIT           NULL,
    [SOPMARCO]            BIT           NULL,
    [SOPPIES]             BIT           NULL,
    [SOPBELLY]            BIT           NULL,
    [SOPPLANO]            BIT           NULL,
    [SOPVAC]              BIT           NULL,
    [SOPTIMO]             BIT           NULL,
    [SOPMASCARA]          BIT           NULL,
    [SOPOTROS]            BIT           NULL,
    [OTROSSOPORTE]        VARCHAR (100) NULL,
    [POSICION]            INT           NOT NULL,
    [OTROSPOSICION]       VARCHAR (100) NULL,
    [BRAZOS]              INT           NULL,
    [OTROSBRAZOS]         VARCHAR (100) NULL,
    [PIES]                INT           NULL,
    [CABEZA]              INT           NULL,
    [OTROSCABEZA]         VARCHAR (100) NULL,
    [BOLUScm]             INT           NULL,
    [PARAFINA]            BIT           NULL,
    [WALTURA]             VARCHAR (15)  NULL,
    [WLONGITUD]           INT           NULL,
    [PINCLINAGENERAL]     VARCHAR (30)  NULL,
    [PCABEZA]             VARCHAR (30)  NULL,
    [PTATTO]              VARCHAR (30)  NULL,
    [PGLUTEOS]            VARCHAR (30)  NULL,
    [BDERECHOBASE]        VARCHAR (30)  NULL,
    [BDERECHOPOSICION]    VARCHAR (30)  NULL,
    [BDERECHOALTURA]      VARCHAR (30)  NULL,
    [BIZQUIBASE]          VARCHAR (30)  NULL,
    [BIZQUIPOSICION]      VARCHAR (30)  NULL,
    [BIZQUIALTURA]        VARCHAR (30)  NULL,
    [MPOSCION]            VARCHAR (30)  NULL,
    [MFIX]                VARCHAR (30)  NULL,
    [MANGULO]             VARCHAR (30)  NULL,
    [OBSERVACION]         VARCHAR (MAX) NULL,
    [CODCENATESIMULACION] CHAR (10)     NULL,
    CONSTRAINT [PK_HCRADSIMULACION] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_HCRADSIMULACION_ADCENATEN] FOREIGN KEY ([CODCENATESIMULACION]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCRADSIMULACION_HCRADORD] FOREIGN KEY ([IDHCRADORDEN]) REFERENCES [dbo].[HCRADORDEN] ([ID])
);


GO
ALTER TABLE [dbo].[HCRADSIMULACION] NOCHECK CONSTRAINT [FK_HCRADSIMULACION_ADCENATEN];


GO
ALTER TABLE [dbo].[HCRADSIMULACION] NOCHECK CONSTRAINT [FK_HCRADSIMULACION_HCRADORD];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la Observacion ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OBSERVACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el angulo muñeca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'MANGULO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el fix muñeca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'MFIX';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Posicion Muñeca', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'MPOSCION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Altura Brazo Izquierdo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BIZQUIALTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'GuardaPosicion Brazo Izquierdo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BIZQUIPOSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Base BrazoIz quierdo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BIZQUIBASE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Altura Brazo Derecho', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BDERECHOALTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Posicion Brazo Derecho', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BDERECHOPOSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda base Brazo derecho', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BDERECHOBASE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda posicion Gluteos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PGLUTEOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda posicion Tatto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PTATTO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'guarda posicion cabeza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PCABEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la inclinacion general', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PINCLINAGENERAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la longitud', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'WLONGITUD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Altura', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'WALTURA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si tiene Check Parafina', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PARAFINA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda la cantida de Bolus cm', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BOLUScm';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guard Otros Cabeza', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OTROSCABEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Brazos   1 = Neutra    2 = Rotacion      3 =  extension     4 = Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'CABEZA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda posicion 1 = Hacia Gantry      2 = Rana', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Otros Brazos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OTROSBRAZOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Brazos 1 =  alo largo del cuerpo    2 = Sobre la cabeza    3 = sobre el pechco     4 = Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'BRAZOS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Otros Posicion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OTROSPOSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda posicion   1 = Prono    2 = Supina    3 = Otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'POSICION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda Otros Soporte', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OTROSSOPORTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si tiene el soporte Otros   1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPOTROS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si tiene el soporte Mascara  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPMASCARA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si tiene el soporte Timo   1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPTIMO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si tiene el soporte Vac Lok  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPVAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si tiene el soporte Plano inclinado  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPPLANO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si tiene el soporte Belly Board 1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPBELLY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si tiene el soporte Pies 1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPPIES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si tiene el soporte Marco estereotaxico  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPMARCO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si tiene el soporte Holder Prono  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPHOLDER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si tiene el soporte Retractor de Hombros   1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPRETRACTOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si tiene el soporte WING  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPWING';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si tiene el soporte Cojin 1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPCOJIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si tiene el soporte Cara Prono  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPCARAPRONO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si tiene  el soporte OverLay  1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPOVERLAY';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si tiene  el soporte  LEG 1 = true  0 = false  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPDUALLEG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda si tiene  el soporte popliteo 1 = true  0 = false', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'SOPPOPLITEO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda otros protocolo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'OTROPROTOCOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda protocolo   1= Craneo 2 = cabeza y cuello 3= torax 4= abdomen 5 =pelvis 6= extramidades 7 =radio cirugia 8 = otros', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'PROTOCOLO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el id de la orden medica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'IDHCRADORDEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el consecutivo de la tabla', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCRADSIMULACION', @level2type = N'COLUMN', @level2name = N'ID';

