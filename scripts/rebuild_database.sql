USE WebshopDB;
GO

IF OBJECT_ID('OrderLines', 'U') IS NOT NULL DROP TABLE OrderLines;
IF OBJECT_ID('Orders', 'U') IS NOT NULL DROP TABLE Orders;
IF OBJECT_ID('Cart', 'U') IS NOT NULL DROP TABLE Cart;
IF OBJECT_ID('Products', 'U') IS NOT NULL DROP TABLE Products;
GO

CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(800) NULL,
    ImageUrl NVARCHAR(MAX) NULL,
    Category NVARCHAR(50) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    Discount DECIMAL(5, 2) NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Cart (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ProductId INT NOT NULL FOREIGN KEY REFERENCES Products(Id),
    Quantity INT NOT NULL CHECK (Quantity > 0),
    SessionId NVARCHAR(100) NOT NULL,
    AddedAt DATETIME2 NOT NULL DEFAULT GETDATE()
);

CREATE TABLE Orders (
    Id INT PRIMARY KEY IDENTITY(1,1),
    OrderDate DATETIME2 NOT NULL DEFAULT GETDATE(),
    TotalPrice DECIMAL(15, 2) NOT NULL,
    SessionId NVARCHAR(100) NOT NULL
);

CREATE TABLE OrderLines (
    Id INT PRIMARY KEY IDENTITY(1,1),
    OrderId INT NOT NULL FOREIGN KEY REFERENCES Orders(Id),
    ProductId INT NOT NULL FOREIGN KEY REFERENCES Products(Id),
    Quantity INT NOT NULL CHECK (Quantity > 0),
    Price DECIMAL(10, 2) NOT NULL
);

-- Demo data
INSERT INTO Products (Name, Description, ImageUrl, Category, Price, Discount)
VALUES
('Laptop', 'A powerful laptop computer', 'https://example.com/laptop.jpg', 'Electronics', 5000.00, 10),
('Better Laptop', 'A more powerful laptop computer', 'https://example.com/better_laptop.jpg', 'Electronics', 15000.00, 10),
('Headphones', 'Noise-cancelling headphones', 'https://example.com/headphones.jpg', 'Electronics', 1200.00, 15),
('Wireless Mouse', 'A high-performance wireless mouse with adjustable DPI.', 'https://example.com/wireless_mouse.jpg', 'Accessories', 250.00, 10),
('Mechanical Keyboard', 'A backlit mechanical keyboard with customizable keys.', 'https://example.com/mechanical_keyboard.jpg', 'Accessories', 800.00, 15),
('Gaming Laptop', 'A powerful gaming laptop with a high-refresh-rate display.', 'https://example.com/gaming_laptop.jpg', 'Computers', 18000.00, 5),
('USB-C Hub', 'A multi-port USB-C hub with HDMI and Ethernet support.', 'https://example.com/usb_c_hub.jpg', 'Accessories', 400.00, 5),
('4K Monitor', 'A stunning 27-inch 4K monitor with HDR support.', 'https://example.com/4k_monitor.jpg', 'Monitors', 3500.00, 10),
('External SSD', 'A portable 1TB SSD with ultra-fast read and write speeds.', 'https://example.com/external_ssd.jpg', 'Storage', 1500.00, 5),
('Gaming Headset', 'A comfortable gaming headset with surround sound.', 'https://example.com/gaming_headset.jpg', 'Accessories', 1200.00, 10),
('Smartwatch', 'A sleek smartwatch with fitness tracking and notifications.', 'https://example.com/smartwatch.jpg', 'Wearables', 2000.00, 8),
('Webcam', 'A high-definition webcam with noise-cancelling microphone.', 'https://example.com/webcam.jpg', 'Accessories', 500.00, 5),
('Bluetooth Speaker', 'A portable Bluetooth speaker with deep bass.', 'https://example.com/bluetooth_speaker.jpg', 'Audio', 1000.00, 15),
('E-Reader', 'A lightweight e-reader with an anti-glare display.', 'https://example.com/e_reader.jpg', 'Devices', 1200.00, 10),
('Noise-Cancelling Earbuds', 'Wireless earbuds with active noise cancellation.', 'https://example.com/earbuds.jpg', 'Audio', 1500.00, 10),
('VR Headset', 'An immersive virtual reality headset for gaming and apps.', 'https://example.com/vr_headset.jpg', 'Devices', 4000.00, 12),
('Power Bank', 'A 20,000mAh power bank with fast charging support.', 'https://example.com/power_bank.jpg', 'Accessories', 600.00, 10),
('Smartphone', 'A feature-packed smartphone with a cutting-edge camera.', 'https://example.com/smartphone.jpg', 'Phones', 9000.00, 5),
('Tablet', 'A versatile tablet with a 10-inch display.', 'https://example.com/tablet.jpg', 'Devices', 7000.00, 8),
('Portable Projector', 'A compact projector with full HD support.', 'https://example.com/projector.jpg', 'Devices', 3000.00, 5),
('Graphics Card', 'A powerful GPU for gaming and rendering.', 'https://example.com/graphics_card.jpg', 'Components', 12000.00, 5),
('Motherboard', 'A motherboard with advanced overclocking support.', 'https://example.com/motherboard.jpg', 'Components', 3000.00, 8),
('Processor', 'A high-performance CPU with 16 cores.', 'https://example.com/processor.jpg', 'Components', 5000.00, 5),
('Gaming Chair', 'An ergonomic gaming chair with lumbar support.', 'https://example.com/gaming_chair.jpg', 'Furniture', 2000.00, 10),
('Desk Lamp', 'A smart desk lamp with adjustable brightness.', 'https://example.com/desk_lamp.jpg', 'Accessories', 400.00, 5),
('Action Camera', 'A rugged action camera with 4K recording.', 'https://example.com/action_camera.jpg', 'Devices', 2500.00, 10),
('Drone', 'A camera drone with GPS and auto-return functionality.', 'https://example.com/drone.jpg', 'Devices', 8000.00, 5),
('Portable SSD', 'A lightweight 512GB SSD with fast transfers.', 'https://example.com/portable_ssd.jpg', 'Storage', 1200.00, 5),
('Gaming Desk', 'A spacious desk designed for gamers.', 'https://example.com/gaming_desk.jpg', 'Furniture', 5000.00, 10),
('Streaming Microphone', 'A professional USB microphone for streaming.', 'https://example.com/streaming_microphone.jpg', 'Audio', 2000.00, 10),
('Capture Card', 'A capture card for streaming gameplay in HD.', 'https://example.com/capture_card.jpg', 'Accessories', 1500.00, 8),
('Mechanical Hard Drive', 'A 4TB external hard drive.', 'https://example.com/hard_drive.jpg', 'Storage', 1000.00, 5),
('Smart Thermostat', 'A thermostat with app control.', 'https://example.com/thermostat.jpg', 'Smart Home', 2500.00, 5),
('Smart Doorbell', 'A video doorbell with motion detection.', 'https://example.com/doorbell.jpg', 'Smart Home', 2000.00, 10),
('Home Router', 'A Wi-Fi 6 router with excellent range.', 'https://example.com/router.jpg', 'Networking', 2500.00, 10),
('External DVD Drive', 'A slim external DVD drive for laptops.', 'https://example.com/dvd_drive.jpg', 'Accessories', 300.00, 5),
('Soundbar', 'A compact soundbar with virtual surround.', 'https://example.com/soundbar.jpg', 'Audio', 3000.00, 10),
('Fitness Tracker', 'A fitness tracker with heart-rate monitoring.', 'https://example.com/fitness_tracker.jpg', 'Wearables', 1500.00, 10),
('Smart Light Bulb', 'A dimmable smart bulb with app control.', 'https://example.com/light_bulb.jpg', 'Smart Home', 300.00, 5),
('Portable Monitor', 'A 15-inch portable monitor.', 'https://example.com/portable_monitor.jpg', 'Monitors', 2500.00, 8),
('Keyboard Wrist Rest', 'A gel wrist rest for ergonomic typing.', 'https://example.com/wrist_rest.jpg', 'Accessories', 100.00, 5),
('HDMI Cable', 'A high-speed HDMI 2.1 cable.', 'https://example.com/hdmi_cable.jpg', 'Accessories', 200.00, 5),
('USB Flash Drive', 'A 64GB USB flash drive.', 'https://example.com/flash_drive.jpg', 'Storage', 400.00, 10),
('Laptop Stand', 'An adjustable stand for laptops.', 'https://example.com/laptop_stand.jpg', 'Accessories', 500.00, 10),
('Cable Organizer', 'A compact cable management box.', 'https://example.com/cable_organizer.jpg', 'Accessories', 300.00, 5),
('Smart Speaker', 'A voice-controlled smart speaker.', 'https://example.com/smart_speaker.jpg', 'Audio', 2000.00, 8),
('Gaming Mouse Pad', 'A large mouse pad for gaming.', 'https://example.com/mouse_pad.jpg', 'Accessories', 300.00, 10),
('NAS Storage', 'A network-attached storage device.', 'https://example.com/nas_storage.jpg', 'Storage', 5000.00, 8),
('Dual Monitor Arm', 'A monitor arm for dual-screen setups.', 'https://example.com/monitor_arm.jpg', 'Accessories', 1500.00, 10),
('Wi-Fi Extender', 'A Wi-Fi range extender.', 'https://example.com/wifi_extender.jpg', 'Networking', 1000.00, 10);
GO

CREATE INDEX IX_Products_Name ON Products (Name);
CREATE INDEX IX_Products_Description ON Products (Description);
CREATE INDEX IX_Products_Category ON Products (Category);
