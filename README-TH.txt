KURI BLUE - WINDOWS EXE BUILD

วิธีสร้าง EXE โดยไม่ต้องติดตั้ง Visual Studio หรือ .NET บนเครื่อง

1) แตกไฟล์ ZIP นี้
2) เข้า https://github.com แล้วสร้าง Repository ใหม่ (Public หรือ Private ก็ได้)
3) อัปโหลดไฟล์และโฟลเดอร์ทั้งหมดใน ZIP นี้เข้า Repository
   - ต้องเห็น KuriBlue.csproj, Program.cs, MainForm.cs
   - ต้องมีโฟลเดอร์ .github/workflows/build.yml
4) เปิดแท็บ Actions ใน Repository
5) เลือก "Build KuriBlue Windows EXE"
6) ถ้า workflow ยังไม่เริ่ม ให้กด Run workflow
7) รอจนงานเป็นสีเขียว แล้วเปิดงานล่าสุด
8) เลื่อนลงหัวข้อ Artifacts แล้วดาวน์โหลด KuriBlue-Windows-x64
9) แตก ZIP ที่ดาวน์โหลด จะได้ KuriBlue.exe

หมายเหตุ:
- ไฟล์ EXE เป็น Windows x64 และรวม .NET runtime ไว้แล้ว
- โปรแกรมสแกนเฉพาะชื่อ process ที่ Windows เปิดเผยให้เห็น (read-only)
- คำว่า NAME MATCH — REVIEW เป็นเพียงการจับคู่ชื่อ ไม่ใช่หลักฐานว่าโกง
- โปรแกรมไม่อ่าน/แก้ไข memory, ไม่ inject DLL, ไม่ hook เกม และไม่แก้ระบบ
- Windows SmartScreen อาจเตือนกับ EXE ที่ยังไม่มี code-signing certificate
