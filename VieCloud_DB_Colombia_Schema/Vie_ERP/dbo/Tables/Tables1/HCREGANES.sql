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
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCREGANES].[IPCODPACI]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
ADD SENSITIVITY CLASSIFICATION TO
    [dbo].[HCREGANES].[CODPROSAL]
    WITH (LABEL = 'Confidential - PII', INFORMATION_TYPE = 'National ID');


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador de relación con tabla HCHISOACA (historia clínica anestésica). Se registra cuando tipo de registro es 2 (Guardado con Folio) o 3 (Finalizado y Confirmado). FK a HCHISOACA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Relacion con la tabla de HCHISOACA, este campo se registra cuando el Tipo de Registro sea:   2 - Guardado con Folio ó   3 - Finalizado y Confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'IDHCHISPACA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de folio único del registro de anestesia. Se asigna cuando tipo de registro es 2 (Guardado con Folio) o 3 (Finalizado y Confirmado). NCHAR(10).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Folio, este campo se registra cuando el Tipo de Registro sea   2 - Guardado con Folio ó   3 - Finalizado y Confirmado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'NUMEFOLIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca de tiempo (DATETIME) cuando se creó o modificó el registro de anestesia en el sistema. Auditoría de documento clínico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha cuando se realizo el registro. ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'FECHAREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador del registro original de anestesia. Vincula versiones modificadas al registro padre original. Permite trazabilidad de cambios: NULL=registro padre, valor>0=modificación del registro padre especificado. INT, FK a HCREGANES.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'IDREGISTROPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Significa que este fue el primer registro que se realizo ejemplo:     1 Guardo Registro Antestesia (Registro Padre  NULL)  2 Modificaron el Registro de Anestesia ( Id Registro padre 1 )  3 Modificaron el Registro de Anestesia ( Id Registro padre 1 )  4 Modificaron el Registro de Anestesia ( Id Registro padre 1 )  5 Guardaron Con Folio ( Id Registro padre 1 )    No damos cuenta que el Padre es uno y puede tener variaciones que debemos Identificar a que padre pertenece.    ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'IDREGISTROPADRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'IDREGISTROPADRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del registro de anestesia. INT: 1=Guardado Temporalmente, 2=Guardado con Folio, 3=Finalizado y Confirmado. Determina validez y cierre del documento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TIPOREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'1 - Guardado Temporalmente   2 - Guardado con Un Folio   3 - Fianlizado y Confirmado ', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TIPOREGISTRO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TIPOREGISTRO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Técnica de anestesia por sedación aplicada (booleano: 0=No, 1=Sí). BIT. Sedación consciente o profunda.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESSED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Técnica de Anestesia - Sedación --> 0 = No , 1 = Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESSED';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESSED';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Técnica de anestesia general aplicada (booleano: 0=No, 1=Sí). BIT. Anestesia general balanceada o venosa.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Técnica de Anestesia - General --> 0 = No , 1 = Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESGEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESGEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Técnica de anestesia local aplicada (booleano: 0=No, 1=Sí). BIT. Infiltración o bloqueo local.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESLOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Técnica de Anestesia - Local --> 0 = No , 1 = Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESLOC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESLOC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Técnica de anestesia regional aplicada (booleano: 0=No, 1=Sí). BIT. Bloqueo regional periférico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Técnica de Anestesia - Regional --> 0 = No , 1 = Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESREG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESREG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Técnica de anestesia caudal aplicada (booleano: 0=No, 1=Sí). BIT. Bloqueo caudal del sacro.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESCAU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Técnica de Anestesia - Caudal --> 0 = No , 1 = Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESCAU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESCAU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Técnica de anestesia peridural (epidural) aplicada (booleano: 0=No, 1=Sí). BIT. Catéter peridural.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESPER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Técnica de Anestesia - Peridural --> 0 = No , 1 = Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESPER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESPER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Técnica de anestesia raquídea (subaracnoidea) aplicada (booleano: 0=No, 1=Sí). BIT. Punción lumbar intratecal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESRAQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Técnica de Anestesia - Raquídea --> 0 = No , 1 = Si', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESRAQ';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECANESRAQ';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración total de la anestesia en minutos. INT. Desde hora inicial hasta hora final de anestesia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DURANEMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duracion de la Anestsia en Minutos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DURANEMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DURANEMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración total de la cirugía en minutos. INT. Desde hora inicial hasta hora final de procedimiento quirúrgico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DURCIRMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duracion de la Cirugia en Minutos', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DURCIRMIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DURCIRMIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del profesional médico (anestesiólogo) que administró la anestesia. VARCHAR(20), MASKED con Identification_Ofuscado (PII). FK a PRPROFESA.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Profesional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'CODPROSAL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'CODPROSAL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Notas clínicas, incidentes o hallazgos relevantes durante anestesia y procedimiento. VARCHAR(8000). Campo libre para documentación adicional.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'OBSERVACIO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de administración continua del agente anestésico (infusión continua vs. dosis única). CHAR(10). Sí/No.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'CONTINUA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Continuacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'CONTINUA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'CONTINUA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dosis única administrada del agente anestésico (cantidad en unidades del medicamento). CHAR(10). Ej: 5mg, 100mcg.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DOSISUNICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Dosis Unica', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DOSISUNICA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DOSISUNICA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Concentración del agente anestésico en solución (mg/mL, %). CHAR(30). Potencia del medicamento usado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'CONCENTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Concentracion de Anestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'CONCENTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'CONCENTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad total de agente anestésico utilizado en el acto anestésico. CHAR(30). Volumen o peso total.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad de Anestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'CANTIDAD';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'CANTIDAD';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nombre o código del agente anestésico farmacológico utilizado. CHAR(40). Ej: Propofol, Sevoflurano, Bupivacaína, Lidocaína.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'AGENTANES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Agente de Anestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'AGENTANES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'AGENTANES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Nivel de profundidad anestésica alcanzado o clasificación (superficial, moderado, profundo). CHAR(30). Escala clínica de anestesia.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'NIVELANEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Nivel Anestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'NIVELANEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'NIVELANEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Sitio anatómico de punción para bloqueo regional o neuroaxial. CHAR(10). Ej: L3-L4, S1, caudal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'SITIOPUNC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sitio Punc', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'SITIOPUNC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'SITIOPUNC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número de intentos o aguja utilizada para la punción anestésica. INT. Indicador de dificultad técnica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'NUMAGUJA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero de Aguja', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'NUMAGUJA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'NUMAGUJA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Campo obsoleto desde 14-MAY-2020. Antes almacenaba tipo de técnica (1-Peridural, 2-Raquídea, 3-Caudal, 4-Troncal, 5-Local, 6-General). Ahora NULL; usar columnas TECANESXXX individuales. INT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECNANESTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Obsoleto desde 14/Mayo/2020, se guarda ahora en NULL y se utilizan individualmente en nuevos registros de Tecnicas de Anestesia  ------    1-Peridural 2-Raquidia 3-Caudal 4-Troncal 5-LocaL 6-General', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECNANESTE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'TECNANESTE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración de la anestesia en horas. INT. Complemento a DURANEMIN. Conversión: horas enteras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DURAANEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duracion de la Anestesia en horas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DURAANEST';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DURAANEST';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Duración de la cirugía en horas. INT. Complemento a DURCIRMIN. Conversión: horas enteras.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DURACIRUG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Duracion de la Cirugia en horas', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DURACIRUG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DURACIRUG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora y minuto de finalización de la anestesia (DATETIME). Momento en que se suspende administración anestésica.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'HORFINANES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Final Anestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'HORFINANES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'HORFINANES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora y minuto de inicio de la anestesia (DATETIME). Momento del primer agente anestésico administrado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'HORINIANES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Inicial de la Anestesia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'HORINIANES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'HORINIANES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora y minuto de finalización del procedimiento quirúrgico (DATETIME). Cierre de herida o fin de acto.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'HORFINCIRU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Final de la Cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'HORFINCIRU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'HORFINCIRU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Hora y minuto de inicio del procedimiento quirúrgico (DATETIME). Incisión o inicio de intervención.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'HORINICIRU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Hora Inicial de la Cirugia', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'HORINICIRU';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'HORINICIRU';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico (CIE-10, CUPS) registrado después del procedimiento quirúrgico. CHAR(20). Diagnóstico postoperatorio.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DIAGNPOSO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico PosOperatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DIAGNPOSO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DIAGNPOSO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código diagnóstico (CIE-10, CUPS) registrado antes del procedimiento quirúrgico. CHAR(20). Diagnóstico preoperatorio indicación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DIAGNPREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Diagnostico Preoperatorio', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DIAGNPREO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'DIAGNPREO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número único de ingreso del paciente a la institución. CHAR(10). Identificador del episodio de atención/hospitalización.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Ingreso del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'NUMINGRES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'NUMINGRES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la unidad funcional (departamento, servicio, área clínica) donde se realizó la cirugía. CHAR(10). Cirugía, ginecología, traumatología, etc.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Unidad Funcional', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'UFUCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del centro o sede de atención donde ocurrió la intervención. CHAR(10). Ubicación física de la prestación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Centro Atencion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'CODCENATE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'CODCENATE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del paciente (identificación: cédula, pasaporte, documento de identidad). VARCHAR(25), MASKED con Identification_Ofuscado (PII). FK a INPACIENT.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Paciente', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'IPCODPACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'IPCODPACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (IDENTITY) del registro de anestesia. INT PRIMARY KEY. Llave primaria de la tabla HCREGANES.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'IDANESTES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Campo Autonumerico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'IDANESTES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES', @level2type = N'COLUMN', @level2name = N'IDANESTES';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Registro anestésico de los procedimientos quirúrgicos: guarda la información clínica de cada anestesia aplicada durante una cirugía, incluyendo tiempos de inicio y fin, técnica anestésica utilizada, agentes y dosis empleadas, diagnósticos pre y postoperatorios, y el profesional de salud responsable, asociada al ingreso y paciente correspondiente.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'HCREGANES';
