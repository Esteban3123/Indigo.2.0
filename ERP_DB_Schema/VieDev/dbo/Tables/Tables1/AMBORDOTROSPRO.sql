CREATE TABLE [dbo].[AMBORDOTROSPRO] (
    [ID]                       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IDCITA]                   INT                                                                              NULL,
    [ESTADO]                   INT                                                                              NOT NULL,
    [NUMINGRES]                CHAR (10)                                                                        NOT NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                CHAR (10)                                                                        NOT NULL,
    [FECHAREG]                 DATETIME                                                                         NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NOT NULL,
    [CANTIDAD]                 INT                                                                              NOT NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    [GENCAREGROUP]             INT                                                                              NULL,
    [GENCONENTITY]             INT                                                                              NULL,
    [GENINVOICE]               VARCHAR (20)                                                                     NULL,
    [GENINVOICEID]             INT                                                                              NULL,
    [GENSERVICEORDER]          INT                                                                              NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [CODESPECI]                CHAR (3)                                                                         NULL,
    [CODCENATEPRO]             CHAR (10)                                                                        NULL,
    [UFUCODIGOPRO]             CHAR (10)                                                                        NULL,
    [CODUSUPRO]                CHAR (20)                                                                        NULL,
    [FECHAPRO]                 DATETIME                                                                         NULL,
    [INTERPRETACION]           VARCHAR (4000)                                                                   NULL,
    [CODPROSALINT]             CHAR (20)                                                                        NULL,
    [NUMEFOLIOINT]             CHAR (10)                                                                        NULL,
    [MEDICOREALI]              CHAR (20)                                                                        NULL,
    [CODESPREALI]              CHAR (3)                                                                         NULL,
    [FECHAREALI]               DATETIME                                                                         NULL,
    [CORRELACION]              TINYINT                                                                          NULL,
    [OBSERVACIONCORRELA]       VARCHAR (4000)                                                                   NULL,
    [OBSERVACION]              VARCHAR (4000)                                                                   NULL,
    [IDAREAPRO]                INT                                                                              NULL,
    [NOMARCPAT]                VARCHAR (250)                                                                    NULL,
    [PRILLAMADO]               BIT                                                                              CONSTRAINT [DF_AMBORDOTROSPRO_PRILLAMADO] DEFAULT ((0)) NULL,
    [SEGLLAMADO]               BIT                                                                              CONSTRAINT [DF_AMBORDOTROSPRO_SEGLLAMADO] DEFAULT ((0)) NULL,
    [TERLLAMADO]               BIT                                                                              CONSTRAINT [DF_AMBORDOTROSPRO_TERLLAMADO] DEFAULT ((0)) NULL,
    [FECPRILLAMADO]            DATETIME                                                                         NULL,
    [FECSEGILLAMADO]           DATETIME                                                                         NULL,
    [FECTERLLAMADO]            DATETIME                                                                         NULL,
    [PROFPRIMERLLAMADO]        CHAR (20)                                                                        NULL,
    [PROFSEGLLAMADO]           CHAR (20)                                                                        NULL,
    [PROFTERLLAMADO]           CHAR (20)                                                                        NULL,
    [OBVAUSENT]                VARCHAR (MAX)                                                                    NULL,
    [PROAUSENT]                CHAR (20)                                                                        NULL,
    [FECAUSENT]                DATETIME                                                                         NULL,
    [AbsenceReasonCode]        CHAR (4)                                                                         NULL,
    CONSTRAINT [PK_AMBORDOTROSPRO] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_AMBORDOTROSPRO_ADcenaten] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AMBORDOTROSPRO_ADcenaten2] FOREIGN KEY ([CODCENATEPRO]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_AMBORDOTROSPRO_Area] FOREIGN KEY ([IDAREAPRO]) REFERENCES [dbo].[HCAREASC] ([ID]),
    CONSTRAINT [FK_AMBORDOTROSPRO_Cita] FOREIGN KEY ([IDCITA]) REFERENCES [dbo].[AGASICITA] ([CODAUTONU]),
    CONSTRAINT [FK_AMBORDOTROSPRO_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_AMBORDOTROSPRO_INESPECIA] FOREIGN KEY ([CODESPECI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_AMBORDOTROSPRO_INESPECIA2] FOREIGN KEY ([CODESPREALI]) REFERENCES [dbo].[INESPECIA] ([CODESPECI]),
    CONSTRAINT [FK_AMBORDOTROSPRO_Ingreso] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_AMBORDOTROSPRO_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_AMBORDOTROSPRO_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_AMBORDOTROSPRO_INPROFSAL2] FOREIGN KEY ([CODPROSALINT]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_AMBORDOTROSPRO_INPROFSAL3] FOREIGN KEY ([MEDICOREALI]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_AMBORDOTROSPRO_UnidadFuncional] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO]),
    CONSTRAINT [FK_AMBORDOTROSPRO_UnidadFuncional2] FOREIGN KEY ([UFUCODIGOPRO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[AMBORDOTROSPRO] NOCHECK CONSTRAINT [FK_AMBORDOTROSPRO_ADcenaten];


GO
ALTER TABLE [dbo].[AMBORDOTROSPRO] NOCHECK CONSTRAINT [FK_AMBORDOTROSPRO_ADcenaten2];


GO
ALTER TABLE [dbo].[AMBORDOTROSPRO] NOCHECK CONSTRAINT [FK_AMBORDOTROSPRO_Area];


GO
ALTER TABLE [dbo].[AMBORDOTROSPRO] NOCHECK CONSTRAINT [FK_AMBORDOTROSPRO_Cita];


GO
ALTER TABLE [dbo].[AMBORDOTROSPRO] NOCHECK CONSTRAINT [FK_AMBORDOTROSPRO_INCUPSIPS];


GO
ALTER TABLE [dbo].[AMBORDOTROSPRO] NOCHECK CONSTRAINT [FK_AMBORDOTROSPRO_INESPECIA];


GO
ALTER TABLE [dbo].[AMBORDOTROSPRO] NOCHECK CONSTRAINT [FK_AMBORDOTROSPRO_INESPECIA2];


GO
ALTER TABLE [dbo].[AMBORDOTROSPRO] NOCHECK CONSTRAINT [FK_AMBORDOTROSPRO_Ingreso];


GO
ALTER TABLE [dbo].[AMBORDOTROSPRO] NOCHECK CONSTRAINT [FK_AMBORDOTROSPRO_INPACIENT];


GO
ALTER TABLE [dbo].[AMBORDOTROSPRO] NOCHECK CONSTRAINT [FK_AMBORDOTROSPRO_INPROFSAL];


GO
ALTER TABLE [dbo].[AMBORDOTROSPRO] NOCHECK CONSTRAINT [FK_AMBORDOTROSPRO_INPROFSAL2];


GO
ALTER TABLE [dbo].[AMBORDOTROSPRO] NOCHECK CONSTRAINT [FK_AMBORDOTROSPRO_INPROFSAL3];


GO
ALTER TABLE [dbo].[AMBORDOTROSPRO] NOCHECK CONSTRAINT [FK_AMBORDOTROSPRO_UnidadFuncional];


GO
ALTER TABLE [dbo].[AMBORDOTROSPRO] NOCHECK CONSTRAINT [FK_AMBORDOTROSPRO_UnidadFuncional2];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Codigo del motivo de anulacion (codigo de la tabla HCMOANULB) -->23-05-2025 - PBI 26684 Modificación a la validación de los servicios eliminados en la "Nota de otros procedimientos" <--', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'AbsenceReasonCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Correlación  Determina si se maneja correlación :   1-Si   2-No   3-Sin especificar ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'CORRELACION';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado:   
1: Ordenado  
2: Realizado (Completado)  
3: Interpretado  
4: Sin Interfaz  
5: Anulado ó Ausente  
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'AMBORDOTROSPRO', @level2type = N'COLUMN', @level2name = N'ESTADO';

