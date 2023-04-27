create or alter proc sproc_test(
@flag char(5),
@username VARCHAR(100) = NULL,
@password VARCHAR(100) = NULL
)
AS
BEGIN
	IF @flag = 'test'
	BEGIN
		IF @username IS NULL
			BEGIN
				SELECT '1' CODE,
				'Username is required' Message
				RETURN;
			END

		IF @password IS NULL
			BEGIN
				SELECT '2' CODE,
				'Password is required' Message
				RETURN;
			END

		IF NOT EXISTS (
			SELECT 'X' FROM tbl_FundDetails WHERE 
			Account_Name = @username AND Bank_Name = @password 
		)
		BEGIN
			SELECT 1 CODE
			,'User Not Found' Message
			RETURN;
		END

		IF EXISTS (
			SELECT 'X' FROM tbl_FundDetails WHERE 
			Account_Name = @username AND Bank_Name = @password 
		)
		BEGIN
			SELECT 0 CODE
			,'Success' Message
			RETURN;
		END
	END
END