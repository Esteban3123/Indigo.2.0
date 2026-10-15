CREATE TABLE [Billing].[RevenueControl] (
    [Id]                   INT                                                                              IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [AdmissionNumber]      CHAR (10)                                                                        NOT NULL,
    [PatientCode]          VARCHAR (25) MASKED WITH (FUNCTION = 'partial(0, "Identification_Ofuscado", 0)') NOT NULL,
    [FolioQuantity]        TINYINT                                                                          NOT NULL,
    [TopEventFeeModerator] NUMERIC (18, 2)                                                                  CONSTRAINT [DF_RevenueControl_TopEventFeeModerator] DEFAULT ((0)) NOT NULL,
    [TopEventCopay]        NUMERIC (18, 2)                                                                  CONSTRAINT [DF_RevenueControl_TopEventCopay] DEFAULT ((0)) NOT NULL,
    [TopEventFeeRecovery]  NUMERIC (18, 2)                                                                  CONSTRAINT [DF_RevenueControl_TopEventFeeRecovery] DEFAULT ((0)) NOT NULL,
    [TimeStamp]            ROWVERSION                                                                       NOT NULL,
    CONSTRAINT [PK_RevenueControl__Id] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_RevenueControl__AdmissionNumber]
    ON [Billing].[RevenueControl]([AdmissionNumber] ASC);


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Marca temporal en la que ocurrio determinado evento. Guarda el instante tiempo de la creacion , registro o modificacion de un archivo determinado.', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'TimeStamp';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tope por evento de la cuota de recuperacion, Solo aplica a vinculados', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'TopEventFeeRecovery';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tope por evento a copago', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'TopEventCopay';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Tope por evento de l Cuota moderadora', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'TopEventFeeModerator';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Especifica la cantidad de folios que va tener la orden de servicio', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'FolioQuantity';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Codigo del paciente, esto se saca de tabla de INPACIENT de Crystal', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'PatientCode';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'numero del ingreso del paciente, esto se saca de la tabla ADINGRESO de Crystal', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'AdmissionNumber';


GO
EXECUTE sp_addextendedproperty @name = N'MS_Description', @value = 'Id de la tabla de control', @level0type = N'SCHEMA', @level0name = N'Billing', @level1type = N'TABLE', @level1name = N'RevenueControl', @level2type = N'COLUMN', @level2name = N'Id';

