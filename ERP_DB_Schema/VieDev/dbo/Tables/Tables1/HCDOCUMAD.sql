CREATE TABLE [dbo].[HCDOCUMAD] (
    [CONSECUTI]    NUMERIC (18)                                                                     IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]    VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]    CHAR (10)                                                                        NULL,
    [CODCENATE]    CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]    CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]    CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECPROCES]    DATETIME                                                                         NOT NULL,
    [NOMARCADJ]    VARCHAR (150)                                                                    NOT NULL,
    [ARCHEXTEN]    CHAR (10)                                                                        NOT NULL,
    [OBSERDOCU]    CHAR (500)                                                                       NULL,
    [NUMEFOLIO]    NCHAR (10)                                                                       NULL,
    [CODSERIPS]    CHAR (20)                                                                        NULL,
    [CODUSUARI]    CHAR (20)                                                                        NULL,
    [TIPODOCUM]    VARCHAR (3)                                                                      NULL,
    [IDMUESTRA]    INT                                                                              NULL,
    [HCREFRECEPID] INT                                                                              NULL,
    [CODESPECI]    CHAR (10)                                                                        NULL,
    CONSTRAINT [PK_HCDOCUMAD] PRIMARY KEY CLUSTERED ([CONSECUTI] ASC),
    CONSTRAINT [FK_INCUPIPS_CODSERIPS] FOREIGN KEY ([CODSERIPS]) REFERENCES [dbo].[INCUPSIPS] ([CODSERIPS]),
    CONSTRAINT [RX_paciente] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tipo de Documento:  Laboratorio = 2  Imagenologia = 3  Patologias = 4  Decreto_3047= 5  Plantillas_Documentos= 6  Consentimiento_Informado = 7  Resultados_Examenes_Sitio = 8  Lectura_Imagenes= 9  Solicitudes= 10  FacturasDeVenta= 11  Los consecutivos de aqui en adelante son los que cada cliente tiene creados por medio del Formulario llamado Categoria Documentos que se encuentra en Historias clinicas en la parte de archivo.    OtrosProcedimientos = 98   Referencia = 99    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCDOCUMAD', @level2type = N'COLUMN', @level2name = N'TIPODOCUM';

