CREATE TABLE [dbo].[HCREGANES] (
    [IDANESTES]       INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [IPCODPACI]       VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [CODCENATE]       CHAR (10)                                                                        NOT NULL,
    [UFUCODIGO]       CHAR (10)                                                                        NOT NULL,
    [NUMINGRES]       CHAR (10)                                                                        NOT NULL,
    [DIAGNPREO]       CHAR (20)                                                                        NOT NULL,
    [DIAGNPOSO]       CHAR (20)                                                                        NOT NULL,
    [HORINICIRU]      DATETIME                                                                         NOT NULL,
    [HORFINCIRU]      DATETIME                                                                         NOT NULL,
    [HORINIANES]      DATETIME                                                                         NOT NULL,
    [HORFINANES]      DATETIME                                                                         NOT NULL,
    [DURACIRUG]       INT                                                                              NULL,
    [DURAANEST]       INT                                                                              NULL,
    [TECNANESTE]      INT                                                                              NULL,
    [NUMAGUJA]        INT                                                                              NULL,
    [SITIOPUNC]       CHAR (10)                                                                        NULL,
    [NIVELANEST]      CHAR (30)                                                                        NULL,
    [AGENTANES]       CHAR (40)                                                                        NULL,
    [CANTIDAD]        CHAR (30)                                                                        NULL,
    [CONCENTRA]       CHAR (30)                                                                        NULL,
    [DOSISUNICA]      CHAR (10)                                                                        NULL,
    [CONTINUA]        CHAR (10)                                                                        NULL,
    [OBSERVACIO]      VARCHAR (8000)                                                                   NULL,
    [CODPROSAL]       CHAR (20) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)')    NOT NULL,
    [DURCIRMIN]       INT                                                                              NULL,
    [DURANEMIN]       INT                                                                              NULL,
    [TECANESRAQ]      BIT                                                                              NULL,
    [TECANESPER]      BIT                                                                              NULL,
    [TECANESCAU]      BIT                                                                              NULL,
    [TECANESREG]      BIT                                                                              NULL,
    [TECANESLOC]      BIT                                                                              NULL,
    [TECANESGEN]      BIT                                                                              NULL,
    [TECANESSED]      BIT                                                                              NULL,
    [TIPOREGISTRO]    INT                                                                              NULL,
    [IDREGISTROPADRE] INT                                                                              NULL,
    [FECHAREGISTRO]   DATETIME                                                                         NULL,
    [NUMEFOLIO]       NCHAR (10)                                                                       NULL,
    [IDHCHISPACA]     INT                                                                              NULL,
    CONSTRAINT [PK_HCREGANES] PRIMARY KEY CLUSTERED ([IDANESTES] ASC),
    CONSTRAINT [FK_HCREGANES_ADINGRESO] FOREIGN KEY ([NUMINGRES]) REFERENCES [dbo].[ADINGRESO] ([NUMINGRES]),
    CONSTRAINT [FK_HCREGANES_HCREGANES] FOREIGN KEY ([IDANESTES]) REFERENCES [dbo].[HCREGANES] ([IDANESTES]),
    CONSTRAINT [FK_HCREGANES_INPACIENT] FOREIGN KEY ([IPCODPACI]) REFERENCES [dbo].[INPACIENT] ([IPCODPACI])
);


GO
ALTER TABLE [dbo].[HCREGANES] NOCHECK CONSTRAINT [FK_HCREGANES_ADINGRESO];


GO
ALTER TABLE [dbo].[HCREGANES] NOCHECK CONSTRAINT [FK_HCREGANES_HCREGANES];


GO
ALTER TABLE [dbo].[HCREGANES] NOCHECK CONSTRAINT [FK_HCREGANES_INPACIENT];


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Relacion con la tabla de HCHISOACA, este campo se registra cuando el Tipo de Registro sea:   2 - Guardado con Folio ó   3 - Finalizado y Confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Numero de Folio, este campo se registra cuando el Tipo de Registro sea   2 - Guardado con Folio ó   3 - Finalizado y Confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Significa que este fue el primer registro que se realizo ejemplo:     1 Guardo Registro Antestesia (Registro Padre  NULL)  2 Modificaron el Registro de Anestesia ( Id Registro padre 1 )  3 Modificaron el Registro de Anestesia ( Id Registro padre 1 )  4 Modificaron el Registro de Anestesia ( Id Registro padre 1 )  5 Guardaron Con Folio ( Id Registro padre 1 )    No damos cuenta que el Padre es uno y puede tener variaciones que debemos Identificar a que padre pertenece.    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'IDREGISTROPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = '1 - Guardado Temporalmente   2 - Guardado con Un Folio   3 - Fianlizado y Confirmado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TIPOREGISTRO';

