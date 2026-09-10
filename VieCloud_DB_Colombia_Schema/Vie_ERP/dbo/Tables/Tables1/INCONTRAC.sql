CREATE TABLE [dbo].[INCONTRAC] (
    [CONCODIGO] CHAR (6)      NOT NULL,
    [CONDESCRI] VARCHAR (100) NOT NULL,
    [CODENTIDA] CHAR (9)      NULL,
    [CODENTADM] CHAR (5)      NULL,
    [CODNITTER] CHAR (15)     NULL,
    [CONCONTAC] VARCHAR (150) NULL,
    [NUMCONTRA] VARCHAR (15)  NULL,
    [RESEVAPRE] VARCHAR (10)  NOT NULL,
    [CONIMPUTA] VARCHAR (15)  NOT NULL,
    [CONTVALOR] MONEY         NOT NULL,
    [FECINICON] DATETIME      NOT NULL,
    [FECFINCON] DATETIME      NOT NULL,
    [OBSERVACI] VARCHAR (500) NULL,
    [OBSERVCON] VARCHAR (500) NULL,
    [CONESTADO] CHAR (1)      NOT NULL,
    [MODOFACT]  CHAR (1)      NOT NULL,
    [CONIMPRIS] CHAR (1)      NOT NULL,
    [TIPLEGALI] BIT           NOT NULL,
    [FECLEGALI] DATETIME      NOT NULL,
    [CONDIRECC] VARCHAR (50)  NULL,
    [MONFACCON] MONEY         NULL,
    [TOMONFACT] CHAR (1)      NOT NULL,
    [CONEMAIL]  VARCHAR (50)  NULL,
    CONSTRAINT [PK_INCONTRAC] PRIMARY KEY CLUSTERED ([CONCODIGO] ASC)
);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico de contacto del contrato, dirección de email para notificaciones y comunicaciones (VARCHAR 50, PII)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONEMAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo electronico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONEMAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONEMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de monto facturado: 1=Total de Servicios, 2=Valor Entidad. Define la base de facturación del contrato (CHAR 1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'TOMONFACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Monto Facturado de 1: Total del Servicios;2:Valor Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'TOMONFACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'TOMONFACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto facturado del contrato, valor total en dinero sujeto a facturación según tipo definido (MONEY, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'MONFACCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Monto Facturado del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'MONFACCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'MONFACCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección física del contrato, ubicación del prestador o entidad contratada (VARCHAR 50, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONDIRECC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Direccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONDIRECC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONDIRECC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de legalización del contrato, cuándo se formalizó legalmente el acuerdo (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'FECLEGALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Legalizacion del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'FECLEGALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'FECLEGALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de legalización: 1=Contrato legalizado, 0=No legalizado. Estado legal del contrato (BIT)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'TIPLEGALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Legalizado 1:Legalizado;2: No Legalizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'TIPLEGALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'TIPLEGALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Imprimir servicios de: 1=IPS, 2=EPS. Define quién aparece en impresión de documentos (CHAR 1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONIMPRIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Imprimir servicios de 1=IPS;2=EPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONIMPRIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONIMPRIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modo de facturación del contrato, método o periodicidad de generación de facturas (CHAR 1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'MODOFACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modo Facturacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'MODOFACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'MODOFACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del contrato: A=Activo, I=Inactivo, C=Cancelado. Indica situación vigente (CHAR 1)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones específicas del contrato, notas administrativas o condiciones adicionales (VARCHAR 500, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'OBSERVCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'OBSERVCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'OBSERVCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones generales, comentarios o detalles relevantes del contrato (VARCHAR 500, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'OBSERVACI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'OBSERVACI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de fin del contrato, fecha de vencimiento o finalización del acuerdo (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'FECFINCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Fin del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'FECFINCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'FECFINCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio del contrato, cuándo comienza la vigencia del acuerdo (DATETIME)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'FECINICON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Inicio del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'FECINICON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'FECINICON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor del contrato, monto total en dinero pactado entre las partes (MONEY)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONTVALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONTVALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONTVALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Imputación del contrato, código de asignación presupuestal o centro de costo (VARCHAR 15)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONIMPUTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Imputacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONIMPUTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONIMPUTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reserva presupuestal del contrato, monto reservado en presupuesto (VARCHAR 10)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'RESEVAPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reserva Presupuestal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'RESEVAPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'RESEVAPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del contrato, identificador único del acuerdo o documento contractual (VARCHAR 15)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'NUMCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'NUMCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'NUMCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contacto del contrato, nombre o teléfono de la persona responsable (VARCHAR 150, nullable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONCONTAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONCONTAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONCONTAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT, número de identificación tributaria de la entidad contratada (CHAR 15, nullable, FK probable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CODNITTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'NIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CODNITTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CODNITTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de entidad administradora, identificador de la EPS o asegurador (CHAR 5, nullable, FK probable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CODENTADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código entidad administradora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CODENTADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CODENTADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad contratada, identificador de IPS o prestador de servicios (CHAR 9, nullable, FK probable)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del contrato, nombre o denominación comercial del acuerdo (VARCHAR 100)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONDESCRI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONDESCRI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código del contrato, identificador único y clave primaria (CHAR 6, PK)', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONCODIGO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC', @level2type = N'COLUMN', @level2name = N'CONCODIGO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contratos vigentes y cerrados con entidades aseguradoras, EPS, ARS o pagadores. Registra las condiciones comerciales de cada contrato: valor pactado, vigencia, modo de facturación y estado, permitiendo controlar la facturación y cartera por contrato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'INCONTRAC';
