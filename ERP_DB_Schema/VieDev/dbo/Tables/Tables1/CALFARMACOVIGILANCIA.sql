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
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1= Si   2= No   3= NO sabe ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'AMPLIARINFORMAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1= Si   2= No   3= NO sabe ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'PACIENTEMISMAREACC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1= Si   2= No   3= NO sabe ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'EVENTODESAPARE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1= Si   2= No   3= NO sabe ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'OTROSFACTORES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1= Si   2= No   3= NO sabe ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'CALFARMACOVIGILANCIA', @level2type = N'COLUMN', @level2name = N'EVENTODESPUES';

