USE [sas]
GO

SELECT * 
  FROM [dbo].[Regions]

GO


USE [sas]
GO

SELECT * 
  FROM [dbo].[Messages]

GO

CREATE FUNCTION dbo.RemoveUrls (@text NVARCHAR(MAX))
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @result NVARCHAR(MAX) = @text
    DECLARE @httpPos INT

    WHILE CHARINDEX('http', @result) > 0
    BEGIN
        SET @httpPos = CHARINDEX('http', @result)
        DECLARE @spacePos INT = CHARINDEX(' ', @result, @httpPos)
        IF @spacePos = 0
            SET @spacePos = LEN(@result) + 1
        SET @result = STUFF(@result, @httpPos, @spacePos - @httpPos, '')
    END

    RETURN @result
END

CREATE FUNCTION dbo.RemoveRetweetPrefix (@text NVARCHAR(MAX))
RETURNS NVARCHAR(MAX)
AS
BEGIN
    DECLARE @pos INT = CHARINDEX(' ', @text)
    IF LEFT(@text, 3) = 'RT ' AND CHARINDEX('@', @text) = 4
        RETURN LTRIM(SUBSTRING(@text, @pos + 1, LEN(@text)))
    RETURN @text
END







UPDATE Events
SET 
  title = LTRIM(RTRIM(
            dbo.RemoveUrls(
              dbo.RemoveRetweetPrefix(title)
            )
          )),
  summary = LTRIM(RTRIM(
              dbo.RemoveUrls(
                dbo.RemoveRetweetPrefix(summary)
              )
            ))
WHERE title LIKE 'RT @%' OR title LIKE '%http%' 
   OR summary LIKE 'RT @%' OR summary LIKE '%http%';


   
UPDATE Messages
SET 
  Content = LTRIM(RTRIM(
            dbo.RemoveUrls(
              dbo.RemoveRetweetPrefix(Content)
            )
          ))
WHERE Content LIKE 'RT @%' OR Content LIKE '%http%' 



INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'Ã—«∆„');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'Õ—«∆ﬁ');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'«‰›Ã«—« ');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'›Ì÷«‰« ');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'ﬁ ·Ï ÊÃ—ÕÏ');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'ÕÊ«œÀ ”Ì—');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'«Œ ÿ«›');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'«Õ Ã«Ã«  Ê„Ÿ«Â—« ');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'«€ Ì«·« ');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'ÕÊ«œÀ √„‰Ì…');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'ÂÃ„«  „”·Õ…');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'”—ﬁ… Ê”ÿÊ');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'ÿﬁ” ”Ì¡');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'«‰ÂÌ«—«  „»«‰Ú');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'≈€·«ﬁ«  «·ÿ—ﬁ');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N' Õ–Ì—«  √„‰Ì…');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'√“„… „Ì«Â ÊﬂÂ—»«¡');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'‰‘«ÿ ⁄”ﬂ—Ì');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'«‘ »«ﬂ«  „Õ·Ì…');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'√Œ»«— «·„Õ«›Ÿ« ');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'„—«ﬁ»… «·ÕœÊœ');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'œ⁄„ ≈‰”«‰Ì');
INSERT INTO Topics (Id, Name) VALUES (NEWID(), N'‰œ«¡«  «” €«À…');
