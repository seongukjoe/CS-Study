# ASP.NET

## ASP.NET란?
> 웹 페이지로 이동하게 되면 웹 상에서 웹 서버와 HTML, CSS, Javascript와 같은 언어를 사용하여 브라우저와 통신하며, 로그인 양식 제출, 버튼과의 상호작용의 작업에서 브라우저는 정보를 다시 웹 서버에 보냅니다.  
> 이와 비슷한 방식으로 웹 서버는 웹 서비스를 사용하여 브라우저, 모바일 디바이스, 기타 웹 서버 등을 비롯한 다양한 클라이언트와 통신할 수 있습니다. API 클라이언트는 HTTP를 통해 서버와 통신하고 JSON 또는 XML과 같은 데이터 형식을 사용하여 정보를 상호교환합니다.  
>**REST: HTTP를 통해 API를 빌드하는 일반적인 패턴**
> REST란 Representational State Transfer의 약자로 웹 서비스를 빌드하기 위한 아키텍처 스타일입니다. 다음과 같은 HTTP 동사를 사용하여 웹 브라우저에서의 작업을 수행합니다.  
* GET - 데이터 검색에 사용
* POST - 데이터의 새 항목 생성
* PUT - 데이터 항목 업데이트
* PATCH - 항목을 수정하는 방법에 대한 지침을 설명하는 방식으로 데이터 항목을 업데이트에 사용
* DELETE - 데이터 항목 삭제  
> REST를 준수하는 웹 서비스 API는 RESTful API라고 정의합니다.  
> ASP.NET Core은 RESTful API이며 기본적으로 HTTPS를 사용하는 프레임워크로 다양한 장점을 가지고 있습니다.  

## ASP.NET 구성하기

### 프로젝트 템플릿 구성 커맨드
```cs
// dotnet cli command
dontnet new webapi --no-https
// --no-https는 HTTPS 인증서 없이 실행되는 앱 개발로 로컬 개발작업을 단순하게 유지
```

> 이후에 다음과 같은 디렉토리가 생성된다.  


|이름| 설명|
|-------|----------|
|Controlles/ | HTTP 엔드포인트로 노출되는 공용 메서드가 있는 클래스를 포함.|
|Program.cs | Main 메서트-앱의 관리 진입점 포함|
|Startup.cs | 서비스 및 앱의 HTTP 요청 파이프라인 구성|
|0000.csproj | 프로젝트 구성 메타데이터 포함|



