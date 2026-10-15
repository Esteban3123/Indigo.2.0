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