CREATE TABLE [Inventory].[RequestParam] (
    [Id]                    INT          IDENTITY (1, 1) NOT NULL,
    [Code]                  VARCHAR (20) NOT NULL,
    [Monday]                BIT          NOT NULL,
    [Tuesday]               BIT          NOT NULL,
    [Wednesday]             BIT          NOT NULL,
    [Thursday]              BIT          NOT NULL,
    [Friday]                BIT          NOT NULL,
    [Saturday]              BIT          NOT NULL,
    [Sunday]                BIT          NOT NULL,
    [Holiday]               BIT          NOT NULL,
    [RequestPeriodicity]    SMALLINT     NOT NULL,
    [AllowExtraRequest]     BIT          NOT NULL,
    [ExtraRequestQuantity]  TINYINT      NOT NULL,
    [State]                 BIT          NOT NULL,
    [CreationUser]          VARCHAR (20) NOT NULL,
    [CreationDate]          DATETIME     NOT NULL,
    [ModificationUser]      VARCHAR (20) NULL,
    [ModificationDate]      DATETIME     NULL,
    [TimeStamp]             ROWVERSION   NOT NULL,
    [Frecuency]             INT          CONSTRAINT [DF__RequestPa__Frecu__4B894806] DEFAULT ((0)) NOT NULL,
    [MeasuryUnitTime]       TINYINT      NULL,
    [InitialDate]           DATETIME     NULL,
    [ClosedDate]            DATETIME     NULL,
    [RequiredAuthorization] BIT          CONSTRAINT [DF__RequestPa__Requi__608464EC] DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_RequestParam] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_RequestParam]
    ON [Inventory].[RequestParam]([Code] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indica si la solicitud de inventario requiere autorización previa (BIT: 1=Sí requiere, 0=No requiere). Parámetro de control de aprobación para solicitudes de suministros, medicamentos o insumos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'RequiredAuthorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Autorización requerida | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'RequiredAuthorization';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'RequiredAuthorization';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de cierre o corte del período de solicitud (DATETIME). Se actualiza automáticamente cuando la periodicidad es mensual o anual. Marca el fin de vigencia del parámetro de solicitud.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'ClosedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha de corte, se actualiza cuando el tipo de unidad es mensual o anual', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'ClosedDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'ClosedDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha de inicio del parámetro de solicitud (DATETIME). Determina cuándo comienza a regir la configuración de periodicidad y frecuencia de solicitudes de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha inicial', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'InitialDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'InitialDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Unidad de tiempo para la frecuencia (TINYINT: días, semanas, meses, años). Define la escala temporal en que se aplica la frecuencia de solicitud de suministros e insumos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'MeasuryUnitTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Unidad de tiempo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'MeasuryUnitTime';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'MeasuryUnitTime';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Frecuencia numérica de solicitud (INT, default=0). Cantidad de períodos (según MeasuryUnitTime) entre cada solicitud automática de inventario, medicamentos o materiales.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Frecuency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Frecuencia', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Frecuency';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Frecuency';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Marca temporal de auditoría (TIMESTAMP). Registra automáticamente el instante exacto de creación, modificación o cambio de estado del registro de parámetros de solicitud.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'TimeStamp';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de la última modificación (DATETIME, nullable). Documenta cuándo se actualizó por última vez la configuración de periodicidad, frecuencia o estado de la solicitud.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'ModificationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'ModificationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que realizó la última modificación (VARCHAR 20, nullable). Identificación del profesional o administrador que cambió los parámetros de solicitud de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Modificación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'ModificationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'ModificationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Fecha y hora de creación (DATETIME). Registra cuándo se estableció inicialmente el parámetro de solicitud para la unidad funcional o centro de atención.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Fecha Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'CreationDate';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'CreationDate';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Usuario que creó el parámetro (VARCHAR 20). Identificación del profesional o administrador que configuró originalmente la periodicidad de solicitud.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Usuario Creación', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'CreationUser';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'CreationUser';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Estado del parámetro de solicitud (BIT: 1=Activo, 0=Inactivo). Controla si la solicitud automática de inventario está vigente o suspendida.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Estado:  1. Activo  0. Inactivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'State';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'State';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Cantidad máxima permitida de solicitudes adicionales (TINYINT). Límite de suministros o medicamentos que pueden solicitarse extraordinariamente fuera de la periodicidad regular.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'ExtraRequestQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Cantidad  de solicitud adicional solicitada', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'ExtraRequestQuantity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'ExtraRequestQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Autoriza solicitudes adicionales fuera de la periodicidad (BIT: 1=Sí permitido, 0=No permitido). Indica si se permiten solicitudes extraordinarias de inventario, medicamentos o insumos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'AllowExtraRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Permitir solicitud adicional | 1 = Si | 0 = No', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'AllowExtraRequest';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'AllowExtraRequest';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Periodicidad de solicitud (SMALLINT). Define el patrón o intervalo regular (diario, semanal, mensual, anual) con que se solicitan medicamentos, suministros o insumos al almacén.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'RequestPeriodicity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Periodicidad de la solicitud', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'RequestPeriodicity';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'RequestPeriodicity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Indicador de día festivo (BIT: 1=Sí es festivo, 0=No es festivo). Especifica si la solicitud de inventario aplica en días festivos o feriados nacionales.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Holiday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Día festivo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Holiday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Holiday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplica solicitud el domingo (BIT: 1=Sí, 0=No). Bandera que determina si el parámetro de solicitud rige los domingos para suministros, medicamentos o insumos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Sunday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Domingo', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Sunday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Sunday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplica solicitud el sábado (BIT: 1=Sí, 0=No). Bandera que determina si el parámetro de solicitud rige los sábados para inventario de medicamentos o materiales.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Saturday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Sabado', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Saturday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Saturday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplica solicitud el viernes (BIT: 1=Sí, 0=No). Bandera que determina si el parámetro de solicitud rige los viernes para reposición de insumos o medicamentos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Friday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Viernes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Friday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Friday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplica solicitud el jueves (BIT: 1=Sí, 0=No). Bandera que determina si el parámetro de solicitud rige los jueves para solicitud de suministros e insumos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Thursday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Jueves', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Thursday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Thursday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplica solicitud el miércoles (BIT: 1=Sí, 0=No). Bandera que determina si el parámetro de solicitud rige los miércoles para reposición de inventario de medicamentos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Wednesday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Miércoles', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Wednesday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Wednesday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplica solicitud el martes (BIT: 1=Sí, 0=No). Bandera que determina si el parámetro de solicitud rige los martes para solicitud de materiales e insumos de farmacia.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Tuesday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Martes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Tuesday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Tuesday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Aplica solicitud el lunes (BIT: 1=Sí, 0=No). Bandera que determina si el parámetro de solicitud rige los lunes para reposición de medicamentos e inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Monday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Lunes', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Monday';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Monday';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Código identificador del parámetro de solicitud (VARCHAR 20, PK). Identificación única y clara de la configuración de periodicidad para solicitud de medicamentos, suministros o insumos.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Código', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Code';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Code';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Identificador único autoincrementable (INT IDENTITY, PK). Clave primaria secuencial que identifica unívocamente cada parámetro de solicitud de inventario en el sistema.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionOriginal', @value = N'Id Autoincrementable de la tabla', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Id';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_enriched_claude-haiku-4-5_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam', @level2type = N'COLUMN', @level2name = N'Id';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = N'Parámetros de configuración para las solicitudes de inventario: define en qué días de la semana se permite solicitar, con qué periodicidad o frecuencia se generan las solicitudes, si se permiten pedidos extraordinarios y si requieren autorización. Controla las reglas operativas de cada código de solicitud de insumos o medicamentos en el módulo de inventario.', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam';
GO
EXECUTE sp_addextendedproperty @name = N'MS_DescriptionSource', @value = N'ai_claude-sonnet-4-6_2026-05-04', @level0type = N'SCHEMA', @level0name = N'Inventory', @level1type = N'TABLE', @level1name = N'RequestParam';
