# DeviceStatsT - .NET Backend + Angular SPA

Приложение для приёма, хранения и просмотра статистики активности устройств.

Проект состоит из двух частей:
- DeviceStatsT - ASP.NET Core Web API (.NET 8), принимает статистику, сохраняет её в PostgreSQL и предоставляет данные для SPA
- DeviceStatsT.Spa - Angular SPA, отображающая список устройств и статистику по выбранному устройству

База данных PostgreSQL запускается в Docker.

## Demo

Frontend: https://device-stats-wffb.onrender.com  
API: https://device-stats-api.onrender.com/swagger

## Функциональность

- Приём статистики активности устройства через REST API
- Сохранение данных в PostgreSQL
- Получение списка устройств
- Получение статистики активности выбранного устройства
- Валидация входных данных и корректные HTTP-ответы
- Swagger для просмотра и тестирования API
- CORS настроен для работы SPA с API

## Технологии

Backend:
- .NET 8
- ASP.NET Core Web API
- PostgreSQL (Npgsql)
- Dapper
- Swagger (OpenAPI)

Frontend:
- Angular
- RxJS
- Standalone components
- Angular Router
- LESS

Infrastructure:
- Docker (PostgreSQL)

## Данные и хранение

Используются таблицы:
- devices: id, name, version
- device_stats: id, device_id, start_time, end_time

Связь:
- device_stats.device_id -> devices.id (FK, cascade)

Время:
- backend использует DateTimeOffset
- PostgreSQL хранит время в формате timestamptz
- frontend отображает даты в локальной таймзоне пользователя (через DatePipe)

## Валидация

Для входного запроса реализованы проверки:
- name - обязательное поле, длина от 3 до 256 символов
- version - обязательное поле, формат версии X.Y.Z или X.Y.Z.W
- EndTime должен быть строго больше StartTime

При невалидных данных API возвращает 400 Bad Request.

## API

### POST /api/Stats

Принимает статистику активности устройства и сохраняет её в базе данных.

Для тестирования отправки статистики можно использовать Swagger (POST /api/Stats).

Пример тела запроса:
```json
{
  "_id": "f695ea23-8662-4a57-975a-f5afd26655db",
  "name": "Device name",
  "startTime": "1980-01-02T00:00:00.000Z",
  "endTime": "1980-01-04T00:00:00.000Z",
  "version": "1.0.0.56"
}
```

Особенности:
- сохранение выполняется в транзакции
- устройство сохраняется через upsert по id (ON CONFLICT (id) DO UPDATE)
- затем добавляется запись в device_stats

Ответы:
- 200 OK - запись сохранена
- 400 Bad Request - невалидные данные

### GET /api/Devices

Возвращает список устройств.

Ответы:
- 200 OK (в том числе пустой список)

### GET /api/devices/{id}/stats

Возвращает интервалы активности выбранного устройства.

Ответы:
- 200 OK (в том числе пустой список)

## Запуск проекта (Windows, cmd)

Для работы приложения необходимо запустить:
1. PostgreSQL (Docker)
2. Backend
3. Frontend

### 1) PostgreSQL (Docker)

Открой cmd и выполни:

```bat
docker run -d ^
  --name devicestats-postgres ^
  -p 5432:5432 ^
  -e POSTGRES_DB=devicestats ^
  -e POSTGRES_USER=postgres ^
  -e POSTGRES_PASSWORD=postgres ^
  postgres:latest
```

Проверка контейнера:
```bat
docker ps
```

Остановка:
```bat
docker stop devicestats-postgres
```

Удаление:
```bat
docker rm devicestats-postgres
```

### 2) Backend (DeviceStatsT)

Перейди в папку backend-проекта:

```bat
cd .\DeviceStatsT
```

Запусти приложение:

```bat
dotnet run
```

Swagger доступен по адресу:
```
https://localhost:7286/swagger
```

Строка подключения к БД берётся из конфигурации:
- ConnectionStrings:DevicestatsDb

### 3) Frontend (DeviceStatsT.Spa)

Открой второй терминал cmd и перейди в папку SPA:

```bat
cd .\DeviceStatsT.Spa
```

Установи зависимости:

```bat
npm install
```

Запусти dev-сервер:

```bat
ng serve
```

SPA будет доступна по адресу:
```
http://localhost:4200
```

Важно: Angular dev-сервер работает, пока открыт терминал. При закрытии окна ng serve останавливается.

## CORS

Для работы SPA с API включён CORS для origin:
- http://localhost:4200
