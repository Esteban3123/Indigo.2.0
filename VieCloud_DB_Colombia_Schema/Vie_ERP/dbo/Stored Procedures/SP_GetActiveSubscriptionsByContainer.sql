CREATE PROCEDURE SP_GetActiveSubscriptionsByContainer
    @Container VARCHAR(10), -- Parámetro para el container
	@CheckDate DATE      -- Parámetro para la fecha de chequeo
AS
BEGIN
	SET NOCOUNT ON;
    SELECT 
        *
    FROM 
        Marketplace.ActiveSubscription
    WHERE IsActive = 1
	AND Container = @Container
	AND (
             -- Estado "Subscribe"
        (SubscriptionStatus = 'Subscribed' AND (
            IsRenewable = 1
            OR (InitialDate <= @CheckDate AND @CheckDate <= FinalDate)
        ))
        -- Estado "Unsubscribe"
        OR (SubscriptionStatus = 'Unsubcribed' AND InitialDate <= @CheckDate AND @CheckDate <= FinalDate)
        -- Estado "Suspend" no retorna registros, por lo que se excluye
	);
END;

GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Purpose', @value=N'Recupera las suscripciones activas y vigentes de un container específico en una fecha dada, considerando el estado de la suscripción y su renovabilidad.', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetActiveSubscriptionsByContainer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Preconditions', @value=N'Debe proporcionarse un identificador de container; Debe proporcionarse una fecha de chequeo para evaluar la vigencia', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetActiveSubscriptionsByContainer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Invariants', @value=N'Solo se devuelven suscripciones con IsActive = 1; Las suscripciones con estado ''Suspend'' nunca se retornan; Una suscripción ''Subscribed'' renovable (IsRenewable=1) siempre es vigente sin importar fechas; El filtrado se realiza siempre sobre un único Container', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetActiveSubscriptionsByContainer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_DomainConcepts', @value=N'Suscripción activa; Marketplace; Container; Estado de suscripción (Subscribed/Unsubcribed/Suspend); Renovación de suscripción; Vigencia de suscripción', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetActiveSubscriptionsByContainer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_SideEffects', @value=N'[RETURN_RESULT] Marketplace.ActiveSubscription: Cuando IsActive=1 y Container coincide y (estado ''Subscribed'' con IsRenewable=1 o fecha dentro del rango Initial/Final, o estado ''Unsubcribed'' con fecha dentro del rango), se retornan todas las columnas del registro', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetActiveSubscriptionsByContainer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Decisions', @value=N'si SubscriptionStatus = ''Subscribed'' y IsRenewable = 1 → Se incluye la suscripción sin validar rango de fechas; si SubscriptionStatus = ''Subscribed'' y IsRenewable = 0 → Se incluye solo si la fecha de chequeo está entre InitialDate y FinalDate; si SubscriptionStatus = ''Unsubcribed'' → Se incluye solo si la fecha de chequeo está entre InitialDate y FinalDate; si SubscriptionStatus = ''Suspend'' (u otro distinto a Subscribed/Unsubcribed) → Se excluye del resultado', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetActiveSubscriptionsByContainer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Consumes', @value=N'Marketplace.ActiveSubscription', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetActiveSubscriptionsByContainer';
-- GO
-- EXEC sys.sp_addextendedproperty @name=N'MS_BR_Source', @value=N'ai_claude-opus-4-7_tier-c_2026-05-06', @level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=N'SP_GetActiveSubscriptionsByContainer';
-- GO
