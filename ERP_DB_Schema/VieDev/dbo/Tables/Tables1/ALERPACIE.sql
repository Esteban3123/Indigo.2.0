CREATE TABLE [dbo].[ALERPACIE] (
    [AUTOALERT] INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI] VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [COMENALER] VARCHAR (500)                                                                    NULL,
    [FECHAREGI] DATETIME                                                                         NOT NULL,
    [CODUSUREG] CHAR (20)                                                                        NOT NULL,
    [FECHAINAC] DATETIME                                                                         NULL,
    [CODUSUINA] CHAR (20)                                                                        NULL,
    [ESTAALERT] CHAR (1)                                                                         NOT NULL,
    CONSTRAINT [PK_ALERPACIE] PRIMARY KEY CLUSTERED ([AUTOALERT] ASC),
    CONSTRAINT [FK_ALERPACIE_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[ALERPACIE] NOCHECK CONSTRAINT [FK_ALERPACIE_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Estado Alerta  1:Activo;  2:Inactivo;', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'ALERPACIE', @level2type = N'COLUMN', @level2name = N'ESTAALERT';

