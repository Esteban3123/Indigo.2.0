CREATE TABLE [dbo].[ADCONCOEX] (
    [CODCONCEC]                NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]                VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IPFECHACO]                DATETIME                                                                         NOT NULL,
    [IPFECHCIT]                DATETIME                                                                         NOT NULL,
    [CONESTADO]                INT                                                                              NOT NULL,
    [IPNOMCOMP]                CHAR (250) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')             NOT NULL,
    [CODENTIDA]                CHAR (9)                                                                         NULL,
    [CODCENATE]                CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]                CHAR (10)                                                                        NOT NULL,
    [CODTIPCON]                INT                                                                              NULL,
    [CODPROSAL]                CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [NUMINGRES]                CHAR (10)                                                                        NOT NULL,
    [NUMCONCIT]                CHAR (20)                                                                        NULL,
    [PRIMERLLA]                BIT                                                                              NULL,
    [SEGUNDLLA]                BIT                                                                              NULL,
    [TERCERLLA]                BIT                                                                              NULL,
    [FECPRILLA]                DATETIME                                                                         NULL,
    [FECSEGLLA]                DATETIME                                                                         NULL,
    [FECTERLLA]                DATETIME                                                                         NULL,
    [OBVAUSENT]                VARCHAR (300)                                                                    NULL,
    [PROAUSENT]                CHAR (20)                                                                        NULL,
    [PROPRILLA]                CHAR (20)                                                                        NULL,
    [PROSEGLLA]                CHAR (20)                                                                        NULL,
    [PROTERLLA]                CHAR (20)                                                                        NULL,
    [FECAUSENT]                DATETIME                                                                         NULL,
    [INDAUDFOR]                NUMERIC (18)                                                                     NOT NULL,
    [AUTESTADO]                INT                                                                              NULL,
    [CODESPECI]                CHAR (3)                                                                         NULL,
    [LIQUIDAR]                 BIT                                                                              NOT NULL,
    [CODSERIPS]                CHAR (20)                                                                        NULL,
    [GENCAREGROUP]             INT                                                                              NULL,
    [GENCONENTITY]             INT                                                                              NULL,
    [GENINVOICE]               VARCHAR (20)                                                                     NULL,
    [GENINVOICEID]             INT                                                                              NULL,
    [IDDESCRIPCIONRELACIONADA] INT                                                                              NULL,
    [FECREGSIS]                DATETIME                                                                         NULL,
    [IDHCHISPACA]              INT                                                                              NULL,
    [IDHCCTRNOTE]              INT                                                                              NULL,
    [IDHCCTRNOTT]              INT                                                                              NULL,
    [FECREGISTROSALA]          DATETIME                                                                         NULL,
    [FECENCONSULTORIO]         DATETIME                                                                         NULL,
    [FECCITACUMPLIDA]          DATETIME                                                                         NULL,
    [IdCitaSync]               VARCHAR (25)                                                                     NULL,
    [CodigoConsultorio]        VARCHAR (100)                                                                    NULL,
    CONSTRAINT [PK_HCCONCOEX] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_ADCONCOEX_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ADCONCOEX_HCCTRNOTE] FOREIGN KEY ([IDHCCTRNOTE]) REFERENCES [dbo].[HCCTRNOTE] ([ID]),
    CONSTRAINT [FK_ADCONCOEX_HCCTRNOTT] FOREIGN KEY ([IDHCCTRNOTT]) REFERENCES [dbo].[HCCTRNOTT] ([ID]),
    CONSTRAINT [FK_ADCONCOEX_HCHISPACA] FOREIGN KEY ([IDHCHISPACA]) REFERENCES [dbo].[HCHISPACA] ([ID]),
    CONSTRAINT [FK_ADCONCOEX_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ADCONCOEX] NOCHECK CONSTRAINT [FK_ADCONCOEX_ADINGRESO];


GO
ALTER TABLE [dbo].[ADCONCOEX] NOCHECK CONSTRAINT [FK_ADCONCOEX_HCCTRNOTE];


GO
ALTER TABLE [dbo].[ADCONCOEX] NOCHECK CONSTRAINT [FK_ADCONCOEX_HCCTRNOTT];


GO
ALTER TABLE [dbo].[ADCONCOEX] NOCHECK CONSTRAINT [FK_ADCONCOEX_HCHISPACA];


GO
ALTER TABLE [dbo].[ADCONCOEX] NOCHECK CONSTRAINT [FK_ADCONCOEX_INPACIENT];


GO
CREATE NONCLUSTERED INDEX [IX_ADCONCOEX_CODCENATE_CODPROSAL_CONESTADO_CODENTIDA_IPCODPACI_IPFECHACO_NUMCONCIT_NUMINGRES_PRIMERLLA_SEGUNDLLA_TERCERLLA]
    ON [dbo].[ADCONCOEX]([CODCENATE] ASC, [CODPROSAL] ASC, [CONESTADO] ASC)
    INCLUDE([CODENTIDA], [IPCODPACI], [IPFECHACO], [NUMCONCIT], [NUMINGRES], [PRIMERLLA], [SEGUNDLLA], [TERCERLLA]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el código del consultorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'CodigoConsultorio';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Guarda el id de la sincronización de la cita', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'IdCitaSync';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1->primera vez  2->control  3->pos operatorio  4->no aplica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'CODTIPCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del Control de Consultas: 
1: Sin Atender  
2: Ausente/Anulada   
3: Atendido   
4: En Sala    
5: En Consultorio 
6: Atendido pero paciente continua en la unidad (nuevo estado 19-05-2023) se creo mas que todo para salud visual, cambio que pidio andres cabrera.  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONCOEX', @level2type = N'COLUMN', @level2name = N'CONESTADO';

