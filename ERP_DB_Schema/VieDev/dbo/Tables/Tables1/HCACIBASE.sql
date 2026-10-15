CREATE TABLE [dbo].[HCACIBASE] (
    [IDACIDBASE] INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]  CHAR (10)                                                                        NOT NULL,
    [FECHREGIS]  DATETIME                                                                         NOT NULL,
    [FEHORAINI]  CHAR (5)                                                                         NOT NULL,
    [CODCENATE]  CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                        NOT NULL,
    [ORDEREGIST] INT                                                                              NOT NULL,
    [CODPROSAL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NULL,
    [MODVENTIL]  CHAR (50)                                                                        NULL,
    [VOLCORRIE]  CHAR (50)                                                                        NULL,
    [VOLMINUTO]  CHAR (50)                                                                        NULL,
    [VOLMINESP]  CHAR (50)                                                                        NULL,
    [PRESISOPO]  CHAR (50)                                                                        NULL,
    [BALANPEEP]  CHAR (50)                                                                        NULL,
    [BALANCPIP]  CHAR (50)                                                                        NULL,
    [FLUJOPICO]  CHAR (50)                                                                        NULL,
    [SENSIBILID] CHAR (50)                                                                        NULL,
    [DISTENSIBI] CHAR (50)                                                                        NULL,
    [BAPMESETA]  CHAR (50)                                                                        NULL,
    [ESTATICA]   CHAR (50)                                                                        NULL,
    [RELACIONIE] CHAR (50)                                                                        NULL,
    [ESFMAZINSP] CHAR (50)                                                                        NULL,
    [FRECPROGR]  CHAR (50)                                                                        NULL,
    [FRECESPON]  CHAR (50)                                                                        NULL,
    [TEMCASCAD]  CHAR (50)                                                                        NULL,
    [BALANFLO2]  CHAR (50)                                                                        NULL,
    [NUMEDETOT]  CHAR (50)                                                                        NULL,
    [COMIDETOT]  CHAR (50)                                                                        NULL,
    [PSSURFACT]  CHAR (50)                                                                        NULL,
    [VIBRACION]  CHAR (50)                                                                        NULL,
    [PHARTERIA]  CHAR (50)                                                                        NULL,
    [PHVENOSO]   CHAR (50)                                                                        NULL,
    [PCO2ARTE]   CHAR (50)                                                                        NULL,
    [PCO2VENOS]  CHAR (50)                                                                        NULL,
    [PO2ARTERI]  CHAR (50)                                                                        NULL,
    [PO2VENOSO]  CHAR (50)                                                                        NULL,
    [HCO3ARTER]  CHAR (50)                                                                        NULL,
    [HCO3VENOS]  CHAR (50)                                                                        NULL,
    [SATO2ARTE]  CHAR (50)                                                                        NULL,
    [SATO2VENO]  CHAR (50)                                                                        NULL,
    [PAO2FLO2]   CHAR (50)                                                                        NULL,
    [BALACCO2]   CHAR (50)                                                                        NULL,
    [BALACAO2]   CHAR (50)                                                                        NULL,
    [BALACVO2]   CHAR (50)                                                                        NULL,
    [BALADAV]    CHAR (50)                                                                        NULL,
    [BALAQSQT]   CHAR (50)                                                                        NULL,
    [SATURACO2]  CHAR (50)                                                                        NULL,
    [TOMCULTIV]  CHAR (50)                                                                        NULL,
    [CTRXTORAX]  CHAR (50)                                                                        NULL,
    [EXTUBACION] CHAR (50)                                                                        NULL,
    [FECHAREGIS] DATETIME                                                                         NULL,
    [OBSERVACIO] CHAR (200)                                                                       NULL,
    [RESITENCI]  CHAR (50)                                                                        NULL,
    [BALANCPH]   CHAR (50)                                                                        NULL,
    [BALANCPL]   CHAR (50)                                                                        NULL,
    [PESIBALON]  CHAR (50)                                                                        NULL,
    [BALANCBE]   CHAR (50)                                                                        NULL,
    [BALANCPIS]  CHAR (50)                                                                        NULL,
    [GRADIEXTR]  CHAR (50)                                                                        NULL,
    [INDICOXIG]  CHAR (50)                                                                        NULL,
    [DELTACO2]   CHAR (50)                                                                        NULL,
    [DELTAHIDR]  CHAR (50)                                                                        NULL,
    [TIEMPINSPI] CHAR (50)                                                                        NULL,
    [TIEMESPIR]  CHAR (50)                                                                        NULL,
    CONSTRAINT [PK_HCACIBASE_1] PRIMARY KEY CLUSTERED ([IDACIDBASE] ASC),
    CONSTRAINT [FK_HCACIBASE_ADCENATEN] FOREIGN KEY ([CODCENATE]) REFERENCES [dbo].[ADCENATEN] ([CODCENATE]),
    CONSTRAINT [FK_HCACIBASE_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCACIBASE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI]),
    CONSTRAINT [FK_HCACIBASE_INPROFSAL] FOREIGN KEY ([CODPROSAL]) REFERENCES [dbo].[INPROFSAL] ([CODPROSAL]),
    CONSTRAINT [FK_HCACIBASE_INUNIFUNC] FOREIGN KEY ([UFUCODIGO]) REFERENCES [dbo].[INUNIFUNC] ([UFUCODIGO])
);


GO
ALTER TABLE [dbo].[HCACIBASE] NOCHECK CONSTRAINT [FK_HCACIBASE_ADCENATEN];


GO
ALTER TABLE [dbo].[HCACIBASE] NOCHECK CONSTRAINT [FK_HCACIBASE_ADINGRESO];


GO
ALTER TABLE [dbo].[HCACIBASE] NOCHECK CONSTRAINT [FK_HCACIBASE_INPACIENT];


GO
ALTER TABLE [dbo].[HCACIBASE] NOCHECK CONSTRAINT [FK_HCACIBASE_INPROFSAL];


GO
ALTER TABLE [dbo].[HCACIBASE] NOCHECK CONSTRAINT [FK_HCACIBASE_INUNIFUNC];


GO
CREATE NONCLUSTERED INDEX [IX_HCACIBASE_IPCODPACI]
    ON [dbo].[HCACIBASE]([IPCODPACI] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCACIBASE_IPCODPACI_NUMINGRES_FECHREGIS_CODCENATE_UFUCODIGO]
    ON [dbo].[HCACIBASE]([IPCODPACI] ASC, [NUMINGRES] ASC, [FECHREGIS] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_HCACIBASE_IPCODPACI_NUMINGRES_FECHREGIS_FEHORAINI_CODCENATE_UFUCODIGO]
    ON [dbo].[HCACIBASE]([IPCODPACI] ASC, [NUMINGRES] ASC, [FECHREGIS] ASC, [FEHORAINI] ASC, [CODCENATE] ASC, [UFUCODIGO] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'identificador consecutivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCACIBASE', @level2type = N'COLUMN', @level2name = N'IDACIDBASE';

