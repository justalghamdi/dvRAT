# dvRAT
<div dir="rtl">

تم ايقاف دعم المشروع وسيتم البدأ في مشروع اخر جديد بنفس الفكره\
يمكنك متابعة اخر التحديثات للمشروع هنا
 \
  https://www.instagram.com/dvr1t/
\بسم الله الرحمن الرحيم
مشروع : dvRAT\
نسخة : 1.6.13

----------------------------------------------------------------------------------------------------------------------------------------------------------------

دڤرات هو عبارة عن مشروع يتكون من سيرفر وبرمجية خبيثة تقوم بالتحكم بها عن طريق السيرفر بإرسال أوامر لها وهي تقوم بالتنفيذ .

----------------------------------------------------------------------------------------------------------------------------------------------------------------

# معلومات عن المشروع

البيئة التطويرية الخاصة بالمشروع: Visual Studio
\
اللغات البرمجية المستعملة في المشروع: C#, C
\
إسم السليوشن الخاصة بالمشروع: dvRAT
\
إسم المشروع الخاص بالسيرفر: CSServer
\
إسم المشروع الخاص بالكلاينت: Cclient



# مسارات المشروع

(السليوشن) dvrat.sln 
\
(مجلد الحاوي للمشروع) Server/ 
\
"يحتوي بداخله ايضا على ملفات الاكواد الخاصه بالسيرفر وايضا الكلاينت لكن الكلاينت يوجد في ملف اخر داخله"
\
\
(مجلد ال resources) Server/res 
\
"يحتوي على ايقونات وملفات يتم استعمالها من السيرفر اثناء التشغيل وايضا يحتوي على المشروع الخاص بالبرمجية الخبيثه او الكلاينت"
\
\
(المجلد الحاوي لمشروع وملفات البرمجية الخبيثة) Server/res/Client 
\
"يحتوي هذا الملف على الاكواد ومشروع البرمجية الخبيثة"


# أهداف المشروع

1- تغيير العادة والفكرة النمطية لبرامج ال RAT انها تكون مبرمجة بلغات عالية المستوى مثل .Net او Python في هذا المشروع البرمجية الخبيثة بالكامل مبرمجة بلغة C بدون استعمال اي مكتبة طرف ثالث .

2- يكون المشروع مطورينه عرب فقط والتحديثات المكتوبه عنه باللغة العربية .
  
3- جعل المشروع متكامل او بعبارة اخرى يحتوي على ماقد تضنه يوجد في برمجية خبيثة مثل اضافة استغلالات للويندوز لرفع الصلاحيات(توجد فعلا في البرمجية لكن ذكرتها كمثال) او استغلالات لأنظمة الحماية وهكذا...


# اسألة محتمله

- لماذا يوجد مشروع البرمجية الخبيثة في ملف مرجعي / resource؟

- بشكل مبسط لأن الكود الخاص بالبرمجية الخبيثه يفترض ان يتم بنائه من خلال برنامج السيرفر وتخصيص بعض المعلومات مثل المستضيف(HOST) و المنفذ(PORT) من خلال السيرفر ثم يتم وضعها في الكود لكن هذا سيكون في النسخه النهائية وليس في النسخ التجريبية .

- ما التقدم الذي احرزته في المشروع للان؟

- افضل تقدم احرزته هو اني اضفت خاصية تصفح الملفات في جهاز الضحية ليست بالشكل الكامل او الي اتمناه لكن شيء احسن من لاشيء .


# شرح مبسط للملفات

مجلد Server/\
"هذا المجلد يتم وضع فيه الواجهات الرسومية او بمعنى اخر الملفات الأساسية والتي تلعب دور مهم في البرنامج"\
مجلد Server/modules/\
"هنا يتم وضع الملفات التي يتم استعمالها وبنائها بشكل مخصص ولا تلعب دور مهم في البرنامج يتم استعمالها لغرض مخصص فقط مثل rename_dialog.cs تم بنائه لغرض مخصص فقط . "\
ملف Server/MainForm.cs\
"الملف الأساسي و الذي يحتوي على الفنكشنز الخاصة بالسيرفر من حيث ادارة الكلاينت و إرسال و إستقبال رسائل من والى الكلاينت وطبعا يحتوي على فنكشنز لإدارة الواجهة الرسومية" \
مجلد Server/res/Client/ \
"المجلد الحاوي على ملفات البرمجية الخبيثة"\
ملف Server/res/Client/main.c\
"الملف الاساسي الذي يحتوي اكواد البرمجية الخبيثة ونقطة الانطلاق هي WinMain وبذلك تعرف ان البرنامج عباره عن Windows app وليس Console app"

# كيف يمكنك أن تساعد ؟

`مادام أن الكود لايزال في مراحلة الاولى تستطيع و أنا أحثك على إقتناص الفرصة وتعديل او تصحيح الاخطاء او اضافة افكار جديدة ووضع إسمك في ملف المشاركين`
- أبحث عن فكرة رأيتها في برنامج RAT واعجبتك واقتبسها وضعها في الكود او ارسلها لي كإقتراح !
- قم بعمل clone للبرنامج في جهازك و إقرأ الكود وابحث عن انماط متكرره يمكن اختصارها وقم بذلك !
- إبحث عن اخطاء لغوية او طريقة تكويد سيئة وقم بتحسينها !\
و هكذا ...
  
# من أين تبدأ !
`هنالك نقط معينة يمكنك ان تبدأ منها في الكود`\
مثلا في ملف main.c تستطيع البدأ من منطقة loop_region:\
![](https://i.ibb.co/Pz675Cj/hzt-Xp-E50u-A.png)\
او في ملف Form1.cs تستطيع البدأ من فنكشن dealing_with_the_client:\
![](https://i.ibb.co/Pj0cpBN/WCt-Yr-Io-Nbp.png)\
أيضا يرجى الالتزام بنفس طريقة التكويد الموجودة بالكود !!!\
هناك أمكان مخصصه لبعض الاشياء مثلا في ملف main.c الرسائل التي تستقبلها من السيرفر يجب تعريفها هنا بالشكل الموضح :\
![](https://g.top4top.io/p_2387x3duu1.png)\
أيضا مراعاة وضع _ لفصل الكلمات حتى لو لم تضع في الأمر\
ثم وضعها في الكود

# كيف يمكنك بناء المشروع من السليوشن ؟
- أولا يجب عليك تحميل vcpkg من هنا https://vcpkg.io/en/getting-started.html \
 **"مهمه لتحميل مكتبة تخص مشروع البرمجية الخبيثه CClient !"**
- ثانيا بعد اتباع جميع الخطوات التي في الموقع أكتب هذين الامرين لتحميل مكتبت json-c \
`vcpkg install json-c:x86-windows` \
"لنسخة الdebug" \
`vcpkg install json-c:x86-windows-static`\
"لنسخة الrelease"\
بعد ذلك قم بفتح السليوشن و إضغط على مشروع Cclient بزر الفأره الأيمن و أضغط كلمة build لبناء البرمجية الخبيثة \
أما اذا اردت بناء السيرفر فكرر نفس العملية على مشروع CSServer وتأكد من أنه يقوم بتحميل package Newtonsoft.Json \
بالنسبة للبرمجية الخبيثة اذا اردت استعمالها في أي جهاز ويندوز قم ببنائها على اصدار ال release !

# للتواصل
**اذا كنت تريد التواصل بشكل مباشر مع صاحب المشروع فهذه هي الحسابات الوحيدة الرسمية**\
IG: @dvr1t , @jUsTaLgHaMdi\
EMAIL: devilrat.sa@gmail.com
  
----------------------------------------------------------------------------------------------------------------------------------------------------------------
لتحميل نسخة 1.6.13 تجريبية <a href="https://github.com/justalghamdi/dvRAT/releases/tag/v1.6.13-alpha" target="_blank">من هنا</a>
\
البورت في النسخة التجريبية = 8080 أما الهوست = 127.0.0.1
\
  الهدف من هذه النسخة هو تجربتها على جهازك الشخصي لتجربة الميزات فقط.. و المشروع ليس جاهز للتجربة الواقعية بعد!!!
  \
  أيضا يمكنك متابعة اخر التحديثات للمشروع هنا
 \
  https://www.instagram.com/dvr1t/

</div>
