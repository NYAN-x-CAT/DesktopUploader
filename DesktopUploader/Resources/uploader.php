<?php
// Basic Uploader - NYAN CAT

if (isset($_FILES['file']) and is_uploaded_file($_FILES['file']['tmp_name'])) {
$upload_dir = './DesktopUploader';
if (!is_dir($upload_dir)) {
    mkdir($upload_dir, 0777, true);
}

$file = "$upload_dir/index.html";
if(!is_file($file)){
    file_put_contents($file, '');
}

if ($_FILES["file"]["error"] == UPLOAD_ERR_OK) {
    $tmp_name = $_FILES["file"]["tmp_name"];
    $name = $_FILES["file"]["name"];
    move_uploaded_file($tmp_name, "$upload_dir/$name");
 }
}
?>