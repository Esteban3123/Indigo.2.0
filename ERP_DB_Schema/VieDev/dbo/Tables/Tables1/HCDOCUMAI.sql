CREATE TABLE [dbo].[HCDOCUMAI] (
    [CONSECUTI] NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES] CHAR (10)                                                                        NOT NULL,
    [CODCENATE] CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO] CHAR (10)                                                                        NOT NULL,
    [CODPROSAL] CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECPROCES] DATETIME                                                                         NOT NULL,
    [NOMARCADJ] CHAR (80)                                                                        NOT NULL,
    [ARCHEXTEN] CHAR (10)                                                                        NOT NULL,
    [OBSERDOCU] CHAR (100)                                                                       NULL,
    [NUMEFOLIO] NCHAR (10)                                                                       NULL,
    [CODSERIPS] CHAR (20)                                                                        NULL,
    [CODUSUARI] CHAR (20)                                                                        NULL,
    [TIPODOCUM] VARCHAR (3)                                                                      NULL,
    [IDMUESTRA] INT                                                                              NULL,
    CONSTRAINT [PK_HCDOCUMAI] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_HCDOCUMAI_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCDOCUMAI_INCUPSIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [FK_HCDOCUMAI_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCDOCUMAI] NOCHECK CONSTRAINT [FK_HCDOCUMAI_ADINGRESO];


GO
ALTER TABLE [dbo].[HCDOCUMAI] NOCHECK CONSTRAINT [FK_HCDOCUMAI_INCUPSIPS];


GO
ALTER TABLE [dbo].[HCDOCUMAI] NOCHECK CONSTRAINT [FK_HCDOCUMAI_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Documento:  Laboratorio = 2  Imagenologia = 3  Patologias = 4  Decreto_3047= 5  Plantillas_Documentos= 6  Consentimiento_Informado = 7  Resultados_Examenes_Sitio = 8  Lectura_Imagenes= 9  Solicitudes= 10  FacturasDeVenta= 11  ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCUMAI', @level2type = N'COLUMN', @level2name = N'TIPODOCUM';

