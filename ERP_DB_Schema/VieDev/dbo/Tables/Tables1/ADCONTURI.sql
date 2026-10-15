CREATE TABLE [dbo].[ADCONTURI] (
    [CODCONCEC]  CHAR (20)                                                                        NOT NULL,
    [IPCODPACI]  VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [IPFECLLEGA] DATETIME                                                                         NOT NULL,
    [CONESTADO]  INT                                                                              NOT NULL,
    [IPPRIAPEL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "FirstSurname_Ofuscado", 0)')      NOT NULL,
    [IPSEGAPEL]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "SecondSurname_Ofuscado", 0)')     NOT NULL,
    [IPPRINOMB]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "FirstName_Ofuscado", 0)')         NOT NULL,
    [IPSEGNOMB]  CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "SecondName_Ofuscado", 0)')        NOT NULL,
    [IPNOMCOMP]  CHAR (250) MASKED WITH (FUNCTION = 'partial(0, "Name_Ofuscado", 0)')             NOT NULL,
    [CODENTIDA]  CHAR (9)                                                                         NULL,
    [CODCENATE]  CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]  CHAR (10)                                                                        NOT NULL,
    [CODTIPPAC]  INT                                                                              NOT NULL,
    [PRIMERLLA]  BIT                                                                              NULL,
    [SEGUNDLLA]  BIT                                                                              NULL,
    [TERCERLLA]  BIT                                                                              NULL,
    [FECPRILLA]  DATETIME                                                                         NULL,
    [FECSEGLLA]  DATETIME                                                                         NULL,
    [FECTERLLA]  DATETIME                                                                         NULL,
    [OBVAUSENT]  VARCHAR (300)                                                                    NULL,
    [PROAUSENT]  CHAR (20)                                                                        NULL,
    [PROPRILLA]  CHAR (20)                                                                        NULL,
    [PROSEGLLA]  CHAR (20)                                                                        NULL,
    [PROTERLLA]  CHAR (20)                                                                        NULL,
    [FECAUSENT]  DATETIME                                                                         NULL,
    [CODUSUARI]  CHAR (20)                                                                        NULL,
    [INDAUDFOR]  NUMERIC (18)                                                                     NOT NULL,
    CONSTRAINT [PK_ADCONTURI] PRIMARY KEY CLUSTERED ([CODCONCEC] ASC),
    CONSTRAINT [FK_ADCONTURI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ADCONTURI] NOCHECK CONSTRAINT [FK_ADCONTURI_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del Tipo de Paciente:  1: Maternas  2: Menores de 5 Años  3: Adultos Mayores  4: Prepagadas o Particulares  5: Poblacion General', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURI', @level2type = N'COLUMN', @level2name = N'CODTIPPAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado del Control de Arribo a Urgencias:  1: Sin Atender  2: Ausente en Clasificacion TRIAGE  3: Clasificado sin Ingreso  4: Clasificado con Ingreso  5: Atendido  6: Ausente en Atencion Inicial Urgencias  7: Anulado por error de Parametrizacion  8: No atendido por Clasificacion III o IV sin Autorizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURI', @level2type = N'COLUMN', @level2name = N'CONESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Fecha de arribo a Urgencias:  se deja NULL cuando es atendido en urgencias sin pasar por triage, se evalua el triage en la Historia Inicial', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ADCONTURI', @level2type = N'COLUMN', @level2name = N'IPFECLLEGA';

