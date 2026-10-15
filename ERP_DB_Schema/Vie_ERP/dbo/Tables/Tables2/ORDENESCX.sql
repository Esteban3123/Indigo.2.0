CREATE TABLE [dbo].[ORDENESCX] (
    [IDETIPHIS]         CHAR (9)                                                                         NOT NULL,
    [NUMEFOLIO]         NCHAR (10)                                                                       NOT NULL,
    [IPCODPACI]         VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [NUMINGRES]         CHAR (10)                                                                        NOT NULL,
    [CODCENATE]         CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]         CHAR (10)                                                                        NOT NULL,
    [CODPROSAL]         CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [FECORDMED]         DATETIME                                                                         NOT NULL,
    [CODSERIPS]         CHAR (20)                                                                        NOT NULL,
    [CANSERIPS]         INT                                                                              NOT NULL,
    [OBSSERIPS]         CHAR (250)                                                                       NULL,
    [PRISERIPS]         CHAR (1)                                                                         NULL,
    [ESTSERIPS]         CHAR (1)                                                                         NULL,
    [MANEXTPRO]         BIT                                                                              NULL,
    [CODDIAGNO]         CHAR (4) MASKED WITH (FUNCTION = 'partial(0, "DiagnosticCode_Ofuscado", 0)')     NULL,
    [INTERPRET]         VARCHAR (4000)                                                                   NULL,
    [CODPROINT]         CHAR (20)                                                                        NULL,
    [NUMFOLINT]         NCHAR (10)                                                                       NULL,
    [SERREAINT]         BIT                                                                              NULL,
    [INDAUDFOR]         NUMERIC (18)                                                                     NULL,
    [ESTALELAB]         BIT                                                                              NULL,
    [FECRECMUE]         DATETIME                                                                         NULL,
    [NOMARCLAB]         CHAR (250)                                                                       NULL,
    [USURECMUE]         CHAR (20)                                                                        NULL,
    [AUTO]              INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IpsAutorizada]     NCHAR (10)                                                                       NULL,
    [Observacion]       NVARCHAR (200)                                                                   NULL,
    [Estado]            INT                                                                              NULL,
    [Radicado]          NCHAR (10)                                                                       NULL,
    [FechaVencimiento]  DATETIME                                                                         NULL,
    [FechaRegQx]        DATETIME                                                                         NULL,
    [FechaCirugia]      DATETIME                                                                         NULL,
    [FechaAnestesia]    DATETIME                                                                         NULL,
    [CirugiaRealizada]  CHAR (10)                                                                        NULL,
    [FechaAutorizacion] DATETIME                                                                         NULL,
    [VistoAnestesia]    CHAR (10)                                                                        NULL,
    [ObservAnestesia]   NVARCHAR (200)                                                                   NULL,
    [CONCURRE]          ROWVERSION                                                                       NULL,
    CONSTRAINT [PK_ORDENESCX__NUMEFOLIO__IPCODPACI__NUMINGRES__CODSERIPS] PRIMARY KEY CLUSTERED ([NUMEFOLIO] ASC, [IPCODPACI] ASC, [NUMINGRES] ASC, [CODSERIPS] ASC),
    CONSTRAINT [FK_ORDENESCX_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_ORDENESCX_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ORDENESCX] NOCHECK CONSTRAINT [FK_ORDENESCX_ADINGRESO];


GO
ALTER TABLE [dbo].[ORDENESCX] NOCHECK CONSTRAINT [FK_ORDENESCX_INPACIENT];

