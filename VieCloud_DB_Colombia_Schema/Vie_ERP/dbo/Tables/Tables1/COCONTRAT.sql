CREATE TABLE [dbo].[COCONTRAT] (
    [CODCONTRA] CHAR (6)        NOT NULL,
    [CODENTIDA] CHAR (9)        NOT NULL,
    [CODENTADM] CHAR (9)        NULL,
    [CODNITTER] CHAR (15)       NULL,
    [CONCONTAC] VARCHAR (150)   NULL,
    [RESEVAPRE] VARCHAR (10)    NULL,
    [CONIMPUTA] VARCHAR (15)    NULL,
    [DESCONTRA] CHAR (90)       NOT NULL,
    [NUMCONTRA] CHAR (15)       NOT NULL,
    [CONFECLEG] DATETIME        NOT NULL,
    [CONTVALOR] NUMERIC (18, 2) NOT NULL,
    [CONFECINI] DATETIME        NOT NULL,
    [CONFECFIN] DATETIME        NOT NULL,
    [CONOBSERV] TEXT            NULL,
    [OBSERVCON] TEXT            NULL,
    [CONESTADO] INT             NULL,
    [MODOFACT]  CHAR (1)        NULL,
    [CONIMPRIS] CHAR (1)        NULL,
    [TIPLEGALI] BIT             NULL,
    [CONDIRECC] VARCHAR (50)    NULL,
    [MONFACCON] MONEY           NULL,
    [TOMONFACT] CHAR (1)        NULL,
    [CONEMAIL]  VARCHAR (50)    NULL,
    [INDAUDFOR] NUMERIC (18, 9) NOT NULL,
    [REGIMEN]   VARCHAR (50)    NULL,
    CONSTRAINT [PK_INCONTRAT] PRIMARY KEY CLUSTERED ([CODCONTRA] ASC),
    CONSTRAINT [FK_COCONTRAT_INENTADM] FOREIGN KEY ([CODENTADM]) REFERENCES [dbo].[INENTADM] ([CODENTADM])
);




GO



GO
CREATE NONCLUSTERED INDEX [_dta_index_COCONTRAT_6_656773447__K2_8]
    ON [dbo].[COCONTRAT]([CODENTIDA] ASC)
    INCLUDE([DESCONTRA]);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Régimen de afiliación (contributivo, subsidiado, vinculado, especial). VARCHAR(50), usado en búsquedas de cobertura y tipo de afiliado.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'REGIMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Régimen', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'REGIMEN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'REGIMEN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de auditoría INDIGO Vie Cloud. NUMERIC(18,9), rastrea conformidad y validación de contrato en sistema.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Auditoria INDIGO', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'INDAUDFOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Correo electrónico del contacto administrativo del contrato. VARCHAR(50), formato PII-Identification_Ofuscado para comunicaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONEMAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Correo Electronico del Contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONEMAIL';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONEMAIL';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo de monto facturado: 1=Total de servicios prestados, 2=Valor facturado a entidad. CHAR(1), determina base de facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'TOMONFACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Monto Facturado de 1: Total del Servicios;2:Valor Entidad', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'TOMONFACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'TOMONFACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Monto facturado del contrato. MONEY, valor económico total cobrado por servicios bajo este contrato.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'MONFACCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Monto Facturado del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'MONFACCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'MONFACCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Dirección del contacto o sede del contrato. VARCHAR(50), ubicación física para comunicaciones y auditoría.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONDIRECC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Direccion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONDIRECC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONDIRECC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Tipo legalización: 1=Contrato legalizado, 0=No legalizado. BIT, indica validez legal y ejecutoriedad del acuerdo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'TIPLEGALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Legalizado 1:Legalizado;2: No Legalizado', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'TIPLEGALI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'TIPLEGALI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Imprime servicio desde: 1=IPS (prestador), 2=EPS (asegurador). CHAR(1), determina origen de impresión de documentos.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONIMPRIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Imprime servicio de 1:IPS;2:EPS', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONIMPRIS';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONIMPRIS';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Modo de facturación del contrato. CHAR(1), establece metodología y frecuencia de cobro (mensual, trimestral, etc).', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'MODOFACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Modo Facturacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'MODOFACT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'MODOFACT';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del contrato: 1=Activo, 2=Suspendido, 3=Inactivo. INT, indica vigencia y operatividad del acuerdo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado del Contrato:  1: Activo  2: Suspendido  3: Inactivo', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONESTADO';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONESTADO';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observaciones y notas del contrato. TEXT, campo para comentarios administrativos, aclaraciones y seguimiento.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'OBSERVCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observaciones del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'OBSERVCON';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'OBSERVCON';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Observación general para el contrato. TEXT, anotaciones generales, restricciones, condiciones especiales.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONOBSERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Observacion General para el Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONOBSERV';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONOBSERV';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha final (vencimiento) del contrato. DATETIME, determina término de vigencia y obligaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONFECFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Final del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONFECFIN';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONFECFIN';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio (vigencia) del contrato. DATETIME, marca comienzo de obligaciones y cobertura.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONFECINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Inicio del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONFECINI';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONFECINI';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Valor total del contrato en pesos. NUMERIC(18,2), monto económico pactado para servicios.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONTVALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Valor del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONTVALOR';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONTVALOR';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de legalización del contrato. DATETIME, registra cuándo el acuerdo adquirió validez legal.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONFECLEG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de Legalizacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONFECLEG';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONFECLEG';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Número del contrato físico (documento). CHAR(15), identificador único del acuerdo en archivo físico.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'NUMCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Numero del Contrato Fisico', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'NUMCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'NUMCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del contrato (objeto, servicios, alcance). CHAR(90), resumen de propósito y cobertura del acuerdo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'DESCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'DESCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'DESCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Imputación contable del contrato. VARCHAR(15), código de asignación presupuestal y contable.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONIMPUTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Imputacion', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONIMPUTA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONIMPUTA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Reserva presupuestal asociada. VARCHAR(10), monto reservado en presupuesto para cumplir obligaciones.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'RESEVAPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Reserva Presupuestal', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'RESEVAPRE';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'RESEVAPRE';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Descripción del contacto del contrato. VARCHAR(150), nombre, cargo y datos del responsable administrativo.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONCONTAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Descripcion del Contacto', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONCONTAC';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CONCONTAC';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'NIT (Número de Identificación Tributaria) de la entidad. CHAR(15), identificador tributario único.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CODNITTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'NIT', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CODNITTER';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CODNITTER';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de la entidad administradora (EPS, ARS). CHAR(9), FK a [dbo].[INENTADM], vinculación con asegurador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CODENTADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo de la Entidad Administradora', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CODENTADM';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CODENTADM';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código de entidad EAPB (Empresa Administradora de Planes de Beneficios). CHAR(9), identificador único de la asegurador.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo Entidad EAPB', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CODENTIDA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CODENTIDA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código único del contrato en sistema. CHAR(6), PK, identificador primario para búsquedas y referencias.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CODCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Codigo del Contrato', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CODCONTRA';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT', @level2type = N'COLUMN', @level2name = N'CODCONTRA';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Contratos con entidades pagadoras (EPS, ARS, aseguradoras, empresas). Registra la información contractual para la facturación y prestación de servicios de salud, incluyendo vigencia, valores pactados y condiciones de facturación.', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'COCONTRAT';
